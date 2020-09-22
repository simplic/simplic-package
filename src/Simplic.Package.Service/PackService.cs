﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
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
            var package = JsonConvert.DeserializeObject<Package>(json);

            return await Pack(package);
        }

        public async Task<byte[]> Pack(Package package)
        {
            using (var stream = new MemoryStream())
            {
                using (var zip = new ZipArchive(stream, ZipArchiveMode.Create))
                {
                    foreach (var item in package.Objects)
                    {
                        // Resolves the concrete registered PackObjectService
                        // Hier exception handeling, falls ein type registriert werden soll, der nicht existiert
                        var packObjectService = container.Resolve<IPackObjectService>(item.Key);

                        // configuration is of Type ObjectListItem
                        foreach (var configuration in item.Value)
                        {
                            var result = await packObjectService.ReadAsync(configuration);

                            // Creates a path inside the zip
                            var entry = zip.CreateEntry(result.Location);

                            using (var resultStream = new MemoryStream(result.File))
                            {
                                // Copy the read file content (byte[]) to the created entry
                                await resultStream.CopyToAsync(entry.Open());
                            }
                        }
                    }
                }
                // Return fuer Test?
                return stream.ToArray();
            }
        }
    }
}
