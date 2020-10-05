using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package.ItemBox
{
    public class UnpackItemBoxService : IUnpackObjectService
    {
        public async Task<UnpackObjectResult> UnpackObject(ExtractArchiveEntryResult extractArchiveEntryResult)
        {
            var result = new UnpackObjectResult();

            try
            {
                var json = Encoding.Default.GetString(extractArchiveEntryResult.Data);
                var deserializedItembox = JsonConvert.DeserializeObject<DeserializedItemBox>(json);

                result.InstallableObject = new InstallableObject
                {
                    Target = extractArchiveEntryResult.Location,
                    Content = deserializedItembox,
                    Mode = extractArchiveEntryResult.Mode
                };

                result.LogLevel = LogLevel.Info;
                result.Message = $"Unpacked ItemBox at {extractArchiveEntryResult.Location}.";
            }
            catch (Exception ex)
            {
                result.LogLevel = LogLevel.Error;
                result.Message = $"Failed to deserialize ItemBox at {extractArchiveEntryResult}.";
                result.Exception = ex;
            }

            return result;
        }
    }
}