using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Configuration;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace Simplic.Package.Service
{
    public class PackService : IPackService
    {
        private readonly IUnityContainer container;

        public PackService(IUnityContainer container)
        {
            this.container = container;
        }

        public async Task<byte[]> Pack(string json)
        {
            var packageConfiguration = JsonConvert.DeserializeObject<PackageConfiguration>(json);

            return await Pack(packageConfiguration);
        }

        public async Task<byte[]> Pack(PackageConfiguration packageConfiguration)
        {
            using (var stream = new MemoryStream())
            {
                using (var zip = new ZipArchive(stream, ZipArchiveMode.Create))
                {
                    // Create entry for package.json
                    var configurationEntry = zip.CreateEntry("package.json");

                    var json = JsonConvert.SerializeObject(packageConfiguration);
                    var jsonBytes = Encoding.Default.GetBytes(json);

                    await WriteToEntry(configurationEntry, jsonBytes);

                    foreach (var item in packageConfiguration.Objects)
                    {
                        // Resolves the concrete registered PackObjectService
                        // Hier exception handeling, falls ein type registriert werden soll, der nicht existiert
                        var packObjectService = container.Resolve<IPackObjectService>(item.Key);

                        foreach (var objectListItem in item.Value)
                        {
                            // await extracts PackObjectResult from Task<PackObjectResult> 
                            var result = await packObjectService.ReadAsync(objectListItem);

                            // Creates a path inside the zip
                            var entry = zip.CreateEntry(result.Location);
                            await WriteToEntry(entry, result.File);
                        }
                    }
                }
                return stream.ToArray();
            }
        }

        /// <summary>
        /// Copies content to a ZipArchiveEntry
        /// </summary>
        /// <param name="entry">A ZipArchiveEntry</param>
        /// <param name="content">The content to copy as an array of bytes</param>
        /// <returns></returns>
        public async Task WriteToEntry(ZipArchiveEntry entry, byte[] content) // TODO: Make private or put into seperate Helper Class
        {
            using (var stream = new MemoryStream(content))
            {
                using (var entryStream = entry.Open())
                {
                    await stream.CopyToAsync(entryStream);
                }
            }
        }
    }
}
