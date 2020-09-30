using Newtonsoft.Json;
using System;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace Simplic.Package.Service
{
    public class PackService : IPackService
    {
        private readonly IUnityContainer container;
        private readonly ILogService logService;
        
        public PackService(IUnityContainer container, ILogService logService)
        {
            this.container = container;
            this.logService = logService;
        }

        public async Task<byte[]> Pack(string json)
        {
            try
            {
                var packageConfiguration = JsonConvert.DeserializeObject<PackageConfiguration>(json);
                return await Pack(packageConfiguration);
            }
            catch (JsonSerializationException jse)
            {
                await logService.WriteAsync("Couldent deserialize the package configuration.", LogLevel.Error);
                throw new PackageConfigurationException("Couldent deserialize the package configuration.", jse);
            }
        }

        public async Task<byte[]> Pack(PackageConfiguration packageConfiguration)
        {
            using (var stream = new MemoryStream())
            {
                using (var archive = new ZipArchive(stream, ZipArchiveMode.Create))
                {
                    // Create entry for package.json
                    var configurationEntry = archive.CreateEntry("package.json");

                    var json = JsonConvert.SerializeObject(packageConfiguration);
                    var jsonBytes = Encoding.Default.GetBytes(json);

                    await WriteToEntry(configurationEntry, jsonBytes);

                    foreach (var item in packageConfiguration.Objects)
                    {
                        var packObjectService = container.Resolve<IPackObjectService>(item.Key);

                        foreach (var objectListItem in item.Value)
                        {
                            var result = await packObjectService.ReadAsync(objectListItem);

                            // Validate the packgedObject before it can be written
                            ValidateObjectResult validateObjectResult = null;
                            try
                            {
                                var validateObjectService = container.Resolve<IValidateObjectService>(item.Key);
                                validateObjectResult = await validateObjectService.Validate(result);
                                await logService.WriteAsync(validateObjectResult.LogMessage, validateObjectResult.LogLevel);

                                if (!validateObjectResult.IsOkay)
                                    throw new InvalidObjectException(validateObjectResult.LogMessage, validateObjectResult.Exception);
                            }
                            catch (ResolutionFailedException)
                            {
                                await logService.WriteAsync($"Skipped Validation on {objectListItem.Source} due to no inability to resolve validation service for {item.Key}", LogLevel.Info);
                            }
                            finally // Write the object to the archive
                            {
                                if (validateObjectResult == null || validateObjectResult.IsOkay)
                                {
                                    var entry = archive.CreateEntry(result.Location);
                                    await WriteToEntry(entry, result.File);
                                }
                            }
                        }
                    }
                }
                File.WriteAllBytes($"{packageConfiguration.Name}_{packageConfiguration.Version}.zip", stream.ToArray());
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