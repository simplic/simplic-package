using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using Unity;

namespace Simplic.Package.Service
{
    public class UnpackService : IUnpackService
    {
        private readonly IUnityContainer container;
        private readonly IFileService fileService;
        private readonly ILogService logService;
        private IValidatePackageConfigurationService validatePackageConfigurationService;

        public UnpackService(IUnityContainer container, IFileService fileService, ILogService logService, IValidatePackageConfigurationService validatePackageConfigurationService)
        {
            this.container = container;
            this.fileService = fileService;
            this.logService = logService;
            this.validatePackageConfigurationService = validatePackageConfigurationService;
        }

        /// <summary>
        /// Unpacks the contents of a given package
        /// </summary>
        /// <param name="packagePath">The path to the package</param>
        /// <returns>A Package object</returns>
        public async Task<Package> Unpack(string packagePath)
        {
            var packageBytes = await fileService.ReadAllBytesAsync(packagePath); // Dont have to throw anything here, just let it throw itself.
            return await Unpack(packageBytes);
        }

        /// <summary>
        /// Unpacks the contents of a given package
        /// </summary>
        /// <param name="packageBytes">The package as bytes</param>
        /// <returns>A Packge object</returns>
        public async Task<Package> Unpack(byte[] packageBytes)
        {
            using (MemoryStream stream = new MemoryStream(packageBytes))
            {
                using (ZipArchive archive = new ZipArchive(stream, ZipArchiveMode.Read))
                {
                    PackageConfiguration packageConfiguration = null;

                    var configurationFile = archive.Entries.Where(x => x.Name == "package.json").FirstOrDefault();
                    if (configurationFile == null)
                        throw new InvalidPackageException("Package doesnt contain package.json file.");

                    var fileContent = await fileService.ReadAllTextAsync(configurationFile.Open());
                    try
                    {
                        packageConfiguration = JsonConvert.DeserializeObject<PackageConfiguration>(fileContent, new JsonSerializerSettings { MissingMemberHandling = MissingMemberHandling.Error });
                    }
                    catch (JsonSerializationException jse)
                    {
                        throw new PackageConfigurationException("Couldent deserialize the package.json file.", jse);
                    }

                    // Validate the PackageConfiguration object
                    var validatePackageConfigurationResult = await validatePackageConfigurationService.Validate(packageConfiguration);
                    if (!validatePackageConfigurationResult.IsValid)
                        throw new PackageConfigurationException(validatePackageConfigurationResult.Message);
                    else
                        await logService.WriteAsync(validatePackageConfigurationResult.Message, validatePackageConfigurationResult.LogLevel);

                    // Create the package object
                    var unpackedPackage = new Package
                    {
                        Name = packageConfiguration.Name,
                        Guid = packageConfiguration.Guid,
                        Version = packageConfiguration.Version,
                        Dependencies = packageConfiguration.Dependencies
                    };

                    
                    // Unpack all the packages content
                    foreach (var item in packageConfiguration.Objects)
                    {
                        var unpackObjectService = container.Resolve<IUnpackObjectService>(item.Key);

                        var contents = new List<InstallableObject>();
                        foreach (var objectListItem in item.Value)
                        {
                            var archiveEntry = archive.GetEntry(objectListItem.Target);
                            if (archiveEntry == null)
                                throw new MissingObjectException($"There was no archive entry found at the location specified: {objectListItem.Target}.");

                            // Extract the main entries content
                            var archiveEntryContent = await fileService.ReadAllBytesAsync(archiveEntry.Open());

                            // Extract the payloads content
                            var payload = new Dictionary<string, byte[]>();
                            if (objectListItem.Payload.Any())
                            {
                                foreach (var _item in objectListItem.Payload)
                                {
                                    var payloadEntry = archive.GetEntry(_item.Target);
                                    if (payloadEntry == null)
                                        throw new MissingObjectException($"There was no archive entry found at the location specified: {_item.Target}.");

                                    payload[_item.Target] = await fileService.ReadAllBytesAsync(payloadEntry.Open());
                                }
                            }

                            // Set the archive entry content
                            var extractArchiveEntryResult = new ExtractArchiveEntryResult
                            {
                                Data = archiveEntryContent,
                                Location = objectListItem.Target,
                                Mode = objectListItem.Mode,
                                Payload = payload
                            };

                            // Unpack the Object (make installable)
                            var unpackObjectResult = await unpackObjectService.UnpackObject(extractArchiveEntryResult);
                            if (unpackObjectResult.InstallableObject == null)
                                throw new InvalidObjectException(unpackObjectResult.Message, unpackObjectResult.Exception);
                            else
                            {
                                await logService.WriteAsync(unpackObjectResult.Message, unpackObjectResult.LogLevel);

                                unpackObjectResult.InstallableObject.Guid = objectListItem.Guid;
                                unpackObjectResult.InstallableObject.PackageGuid = unpackedPackage.Guid;
                                unpackObjectResult.InstallableObject.PackageVersion = unpackedPackage.Version;

                                contents.Add(unpackObjectResult.InstallableObject);
                            }
                        }
                        unpackedPackage.UnpackedObjects[item.Key] = contents;
                    }
                    return unpackedPackage;
                }
            }
        }
    }
}