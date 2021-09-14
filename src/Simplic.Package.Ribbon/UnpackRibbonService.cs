using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package.Ribbon
{
    /// <summary>
    /// Service to unpack ribbon items.
    /// </summary>
    public class UnpackRibbonService : IUnpackObjectService
    {
        /// <inheritdoc/>
        public async Task<UnpackObjectResult> UnpackObject(ExtractArchiveEntryResult extractArchiveEntryResult)
        {
            var result = new UnpackObjectResult
            {
                LogLevel = LogLevel.Info
            };

            try
            {
                var json = Encoding.Default.GetString(extractArchiveEntryResult.Data);
                var ribbon = JsonConvert.DeserializeObject<RibbonTab>(json);

                result.InstallableObject = new InstallableObject
                {
                    Content = ribbon,
                    Target = extractArchiveEntryResult.Location,
                    Mode = extractArchiveEntryResult.Mode
                };

                result.Message = $"Unpacked RibbonTab at {extractArchiveEntryResult.Location}.";
            }
            catch (Exception ex)
            {
                result.Message = $"Failed to unpack RibbonTab at {extractArchiveEntryResult.Location}.";
                result.LogLevel = LogLevel.Error;
                result.Exception = ex;
            }
            return result;
        }
    }
}