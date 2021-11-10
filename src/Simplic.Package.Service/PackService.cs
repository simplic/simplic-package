using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace Simplic.Package.Service
{
    /// <inheritdoc cref="IPackService"/>
    public class PackService : IPackService
    {
        private readonly IUnityContainer container;
        private readonly ILogService logService;
        private readonly IValidatePackageConfigurationService validatePackageConfigurationService;
        private readonly IFileService fileService;

        /// <summary>
        /// Initialize a new instance of <see cref="PackService"/>.
        /// </summary>
        /// <param name="container"></param>
        /// <param name="logService"></param>
        /// <param name="validatePackageConfigurationService"></param>
        /// <param name="fileService"></param>
        public PackService(IUnityContainer container, ILogService logService, IValidatePackageConfigurationService validatePackageConfigurationService, IFileService fileService)
        {
            this.container = container;
            this.logService = logService;
            this.validatePackageConfigurationService = validatePackageConfigurationService;
            this.fileService = fileService;
        }

        /// <summary>
        /// Creates and writes a package based on the package configuration file
        /// </summary>
        /// <param name="json">The package configuration file as a string</param>
        /// <returns>The written archive in bytes</returns>
        public async Task<byte[]> Pack(string json)
        {
            try
            {
                var packageConfiguration = JsonConvert.DeserializeObject<PackageConfiguration>(json);
                return await Pack(packageConfiguration);
            }
            catch (JsonSerializationException jse)
            {
                throw new PackageConfigurationException("Couldent deserialize the package configuration.", jse);
            }
        }

        /// <summary>
        /// Creates and writes a package based on the given PackageConfiguration object
        /// </summary>
        /// <param name="packageConfiguration">The PackageConfiguration to create from</param>
        /// <returns>The written archive in bytes</returns>
        public async Task<byte[]> Pack(PackageConfiguration packageConfiguration)
        {
            // Validate the PackageConfiguration object
            var validatePackageConfigurationResult = await validatePackageConfigurationService.Validate(packageConfiguration);
            if (!validatePackageConfigurationResult.IsValid)
                throw new PackageConfigurationException(validatePackageConfigurationResult.Message);
            else
                await logService.WriteAsync(validatePackageConfigurationResult.Message, validatePackageConfigurationResult.LogLevel);

            if (packageConfiguration.Guid == Guid.Empty)
            {
                var error = $"Empty guid (`{Guid.Empty}`) is not valid package id.";
                await logService.WriteAsync(error, LogLevel.Error);
                throw new PackageConfigurationException(error);
            }

            // Create the package archive
            using (var stream = new MemoryStream())
            {
                using (var archive = new ZipArchive(stream, ZipArchiveMode.Create, true))
                {
                    // Add all objects to the archive
                    foreach (var item in packageConfiguration.Objects)
                    {

                        async Task writeEntry(ObjectListItem _oli, PackObjectResult _por)
                        {
                            // Validate the packgedObject before it can be written
                            ValidateObjectResult validateObjectResult = null;
                            try
                            {
                                var validateObjectService = container.Resolve<IValidateObjectService>(item.Key);
                                validateObjectResult = await validateObjectService.Validate(_por);

                                if (!validateObjectResult.IsValid)
                                    throw new InvalidObjectException(validateObjectResult.Message, validateObjectResult.Exception);
                                else
                                    await logService.WriteAsync(validateObjectResult.Message, validateObjectResult.LogLevel);
                            }
                            catch (ResolutionFailedException)
                            {
                                await logService.WriteAsync($"Skipped Validation on {_oli.Source} due to inability to resolve validation service for {item.Key}", LogLevel.Info);
                            }

                            // Write the object to the archive
                            if (validateObjectResult == null || validateObjectResult.IsValid)
                            {
                                var entry = archive.CreateEntry(_por.Location);
                                await WriteToEntry(entry, _por.File);

                                foreach (var payload in _por.Payload)
                                {
                                    var payloadEntry = archive.CreateEntry(payload.Key);
                                    await WriteToEntry(payloadEntry, payload.Value);
                                }
                            }
                        }

                        var packObjectService = container.Resolve<IPackObjectService>(item.Key);

                        foreach (var objectListItem in item.Value.ToList())
                        {
                            if (!objectListItem.Source.Contains("*"))
                            {
                                var packObjectResult = await packObjectService.ReadAsync(objectListItem);
                                await writeEntry(objectListItem, packObjectResult);
                                continue;
                            }

                            var directory = Path.GetDirectoryName(objectListItem.Source);
                            var fileWildCard = Path.GetFileName(objectListItem.Source);

                            foreach (var fullPath in Directory.GetFiles(directory, fileWildCard, SearchOption.AllDirectories))
                            {
                                var source = fullPath; //Path.Combine(directory, Path.GetFileName(fullPath));

                                var wildCardPOR = new PackObjectResult
                                {
                                    File = await fileService.ReadAllBytesAsync(fullPath),
                                    Location = source
                                };
                                await writeEntry(objectListItem, wildCardPOR);

                                // Add to target
                                item.Value.Add(new ObjectListItem
                                {
                                    Source = source,
                                    Target = source
                                });
                            }

                            item.Value.Remove(objectListItem);

                        }
                    }

                    // Write package config
                    var configurationEntry = archive.CreateEntry("package.json");
                    var jsonSerializerSettings = new JsonSerializerSettings
                    {
                        Converters = new List<JsonConverter> {
                            new VersionConverter()
                        }
                    };

                    var json = JsonConvert.SerializeObject(packageConfiguration, jsonSerializerSettings);
                    var jsonBytes = Encoding.UTF8.GetBytes(json);

                    await WriteToEntry(configurationEntry, jsonBytes);
                }

                string archiveName = $"{packageConfiguration.Name}_v{packageConfiguration.Version}.zip";
                if (fileService.FileExists(archiveName))
                {
                    await logService.WriteAsync($"[TODO: Decide what to do here] A Package with name {packageConfiguration.Name}_v{packageConfiguration.Version} already exists in working directory", LogLevel.Warning);
                    File.Delete(archiveName);
                    await fileService.WriteAllBytesAsync(stream.ToArray(), archiveName);
                    await logService.WriteAsync($"Succesfully created package.", LogLevel.Info);
                }
                else
                {
                    await fileService.WriteAllBytesAsync(stream.ToArray(), archiveName);
                    await logService.WriteAsync($"Succesfully created package {archiveName}.", LogLevel.Info);
                }

                return stream.ToArray();
            }
        }

        /// <summary>
        /// Copies content to a ZipArchiveEntry
        /// </summary>
        /// <param name="entry">A ZipArchiveEntry</param>
        /// <param name="content">The content to write to the entry</param>
        private async Task WriteToEntry(ZipArchiveEntry entry, byte[] content)
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