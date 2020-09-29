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

        public UnpackService(IUnityContainer container, IFileService fileService)
        {
            this.container = container;
            this.fileService = fileService;
        }

        /// <summary>
        /// Unpacks and deserializes the contents of a given package
        /// </summary>
        /// <param name="packagePath">The path to the package</param>
        /// <returns>A UnpackedPackage object</returns>
        public async Task<Package> Unpack(string packagePath)
        {
            var packageBytes = await fileService.ReadAllBytesAsync(packagePath);
            return await Unpack(packageBytes);
        }

        /// <summary>
        /// Unpacks and deserializes the contents of a given package
        /// </summary>
        /// <param name="packageBytes">The package as bytes</param>
        /// <returns>A UnpackedPackage object</returns>
        public async Task<Package> Unpack(byte[] packageBytes)
        {
            using (MemoryStream stream = new MemoryStream(packageBytes))
            {
                using (ZipArchive zipArchive = new ZipArchive(stream, ZipArchiveMode.Read))
                {
                    ZipArchiveEntry configurationFile = null;
                    PackageConfiguration packageConfiguration = null;

                    configurationFile = zipArchive.Entries.Where(x => x.Name == "package.json").FirstOrDefault();

                    if (configurationFile == null)
                        throw new InvalidPackageException("Package doesnt contain a correctly named configuration file");

                    var json = await fileService.ReadAllTextAsync(configurationFile.Open());

                    try
                    {
                        packageConfiguration = JsonConvert.DeserializeObject<PackageConfiguration>(json, new JsonSerializerSettings { MissingMemberHandling = MissingMemberHandling.Error });
                    }
                    catch (JsonSerializationException jse)
                    {
                        throw new PackageConfigurationException("Couldent deserialize the package configuration.", jse);
                    }

                    var unpackedPackage = new Package
                    {
                        Name = packageConfiguration.Name,
                        Version = packageConfiguration.Version,
                        Dependencies = packageConfiguration.Dependencies
                    };

                    var unpackedObjects = new Dictionary<string, IList<InstallableObject>>();
                    foreach (var item in packageConfiguration.Objects)
                    {
                        var unpackObjectService = container.Resolve<IUnpackObjectService>(item.Key);

                        var contents = new List<InstallableObject>();
                        foreach (var objectListItem in item.Value)
                        {
                            var zipEntry = zipArchive.GetEntry(objectListItem.Target);

                            if (zipEntry == null)
                                throw new MissingObjectException(""); // TODO: Excepiton handeling if entry doesnt exist

                            var zipEntryContent = await fileService.ReadAllBytesAsync(zipEntry.Open());

                            var unpackObjectResult = new UnpackObjectResult
                            {
                                Data = zipEntryContent,
                                Location = objectListItem.Target, // TODO: is this needed?
                                Mode = objectListItem.Mode
                            };

                            contents.Add(unpackObjectService.UnpackObject(unpackObjectResult));
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