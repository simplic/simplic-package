using Newtonsoft.Json;
using System.Collections.Generic;
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

        public UnpackService(IUnityContainer container, IFileService fileService, ILogService logService)
        {
            this.container = container;
            this.fileService = fileService;
            this.logService = logService;
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
                    ZipArchiveEntry configurationFile = null;
                    PackageConfiguration packageConfiguration = null;

                    configurationFile = archive.Entries.Where(x => x.Name == "package.json").FirstOrDefault();

                    if (configurationFile == null)
                    {
                        await logService.WriteAsync($"Package doesnt contain a correctly named configuration file", LogLevel.Error);
                        throw new InvalidPackageException("Package doesnt contain a correctly named configuration file");
                    }

                    var json = await fileService.ReadAllTextAsync(configurationFile.Open());
                    try
                    {
                        packageConfiguration = JsonConvert.DeserializeObject<PackageConfiguration>(json, new JsonSerializerSettings { MissingMemberHandling = MissingMemberHandling.Error });
                    }
                    catch (JsonSerializationException jse)
                    {
                        await logService.WriteAsync($"Couldent deserialize the package.json file", LogLevel.Error);
                        throw new PackageConfigurationException("Couldent deserialize the package.json file", jse);
                    }

                    // Create the package
                    var unpackedPackage = new Package
                    {
                        Name = packageConfiguration.Name,
                        Version = packageConfiguration.Version,
                        Dependencies = packageConfiguration.Dependencies
                    };

                    // Unpack all the packages content
                    var unpackedObjects = new Dictionary<string, IList<InstallableObject>>();
                    foreach (var item in packageConfiguration.Objects)
                    {
                        var unpackObjectService = container.Resolve<IUnpackObjectService>(item.Key); // Exception handeling for not beeing able to resolve

                        var contents = new List<InstallableObject>();
                        foreach (var objectListItem in item.Value)
                        {
                            var archiveEntry = archive.GetEntry(objectListItem.Target);

                            if (archiveEntry == null)
                            {
                                await logService.WriteAsync($"There was no archive entry found at {objectListItem.Target}", LogLevel.Error);
                                throw new MissingObjectException($"There was no archive entry found at {objectListItem.Target}");
                            }

                            // Read the archive entry content
                            var archiveEntryContent = await fileService.ReadAllBytesAsync(archiveEntry.Open());

                            var extractArchiveEntryResult = new ExtractArchiveEntryResult
                            {
                                Data = archiveEntryContent,
                                Location = objectListItem.Target,
                                Mode = objectListItem.Mode
                            };

                            // Unpack the Object
                            var unpackObjectResult = await unpackObjectService.UnpackObject(extractArchiveEntryResult);
                            await logService.WriteAsync(unpackObjectResult.LogMessage, unpackObjectResult.LogLevel);

                            if (unpackObjectResult.InstallableObject != null)
                                contents.Add(unpackObjectResult.InstallableObject);
                            else
                                throw new InvalidObjectException(unpackObjectResult.LogMessage, unpackObjectResult.Exception);
                        }
                        unpackedObjects[item.Key] = contents;
                    }
                    unpackedPackage.UnpackedObjects = unpackedObjects;

                    return unpackedPackage;
                }
            }
        }
    }
}