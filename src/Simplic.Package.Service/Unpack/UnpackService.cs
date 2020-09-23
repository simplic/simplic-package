using Newtonsoft.Json;
using Simplic.Package.Model;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
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
        public async Task<UnpackedPackage> Unpack(string packagePath)
        {
            var packageBytes = await fileService.ReadAllBytesAsync(packagePath);
            return await Unpack(packageBytes);
        }

        /// <summary>
        /// Unpacks and deserializes the contents of a given package
        /// </summary>
        /// <param name="packageBytes">The package as bytes</param>
        /// <returns>A UnpackedPackage object</returns>
        public async Task<UnpackedPackage> Unpack(byte[] packageBytes)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                foreach (var _byte in packageBytes)
                {
                    stream.WriteByte(_byte);
                }

                using (ZipArchive zipArchive = new ZipArchive(stream, ZipArchiveMode.Read))
                {
                    var configurationFile = zipArchive.Entries.Where(x => x.Name == "package.json").First();  // TODO: Exception handeling

                    var json = await fileService.ReadAllTextAsync(configurationFile.Open());
                    var packageConfiguration = JsonConvert.DeserializeObject<PackageConfiguration>(json);

                    var unpackedPackage = new UnpackedPackage
                    {
                        Name = packageConfiguration.Name,
                        Version = packageConfiguration.Version,
                        Dependencies = packageConfiguration.Dependencies
                    };

                    var deserializedObjects = new Dictionary<string, IList<IDeserializedContent>>();
                    foreach (var item in packageConfiguration.Objects)
                    {
                        var deserializeObjectService = container.Resolve<IDeserializeObjectService>(item.Key);

                        var deserializedContents = new List<IDeserializedContent>();
                        foreach (var objectListItem in item.Value)
                        {
                            var zipEntry = zipArchive.GetEntry(objectListItem.Target);
                            var zipEntryBytes = await fileService.ReadAllBytesAsync(zipEntry.Open()); // Ggf. wrapper class

                            var unpackObjectResult = new UnpackObjectResult
                            {
                                ReadBytes = zipEntryBytes
                            };

                            var deserializedContent = deserializeObjectService.DeserializeObject(unpackObjectResult);

                            deserializedContents.Add(deserializedContent);
                        }

                        deserializedObjects[item.Key] = deserializedContents;
                    }

                    unpackedPackage.UnpackedObjects = deserializedObjects;

                    return unpackedPackage;
                }
            }
        }
    }
}
}
