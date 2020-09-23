using Newtonsoft.Json;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
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

        public async Task<UnpackedPackage> Unpack(string zipArchivePath)
        {
            var zipArchive = ZipFile.OpenRead(zipArchivePath);
            return await Unpack(zipArchive);
        }

        public async Task<UnpackedPackage> Unpack(ZipArchive zipArchive)
        {
            using (zipArchive)
            {
                var configurationFile = zipArchive.Entries.Where(x => x.Name == "package.json").First(); //TODO: Exception handeling

                var bytes = await fileService.ReadAllBytesAsync(configurationFile);
                var json = Encoding.Default.GetString(bytes);
                var package = JsonConvert.DeserializeObject<Package>(json);

                var unpackedPackage = new UnpackedPackage
                {
                    Name = package.Name,
                    Version = package.Version,
                    Dependencies = package.Dependencies
                };

                var unpackedObjects = new Dictionary<string, IList<UnpackObjectResult>>();
                foreach (var directory in zipArchive.Entries.Where(x => x.FullName.EndsWith("/")))
                {
                    var unpackObjectService = container.Resolve<IUnpackObjectService>(directory.Name); // Potentially directory.Name is empty string

                    var unpackedFiles = new List<UnpackObjectResult>();
                    foreach (var file in zipArchive.Entries.Where(x => x.FullName.StartsWith(directory.Name)))
                    {
                        var unpackObjectResult = await unpackObjectService.UnpackObject(file);
                        unpackedFiles.Add(unpackObjectResult);
                    }

                    unpackedObjects.Add(directory.Name, unpackedFiles);
                }
                unpackedPackage.UnpackedObjects = unpackedObjects;

                return unpackedPackage;
            }
        }
    }
}
