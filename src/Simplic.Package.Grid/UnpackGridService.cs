using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package.Grid
{
    public class UnpackGridService : IUnpackObjectService
    {
        public async Task<UnpackObjectResult> UnpackObject(ExtractArchiveEntryResult extractArchiveEntryResult)
        {
            var result = new UnpackObjectResult { LogLevel = LogLevel.Info};

            try
            {
                var json = Encoding.Default.GetString(extractArchiveEntryResult.Data);
                var content = JsonConvert.DeserializeObject<DeserializedGrid>(json, new JsonSerializerSettings { MissingMemberHandling = MissingMemberHandling.Error }); // TODO: try catch

                result.InstallableObject = new InstallableObject
                {
                    Content = content,
                    Target = extractArchiveEntryResult.Location, // TODO: What to put here? this could be useful
                    Mode = extractArchiveEntryResult.Mode
                };
                result.Message = $"Unpacked grid at {extractArchiveEntryResult.Location}.";
            }
            catch (Exception ex)
            {
                result.Message = $"Failed to unpackc grid at {extractArchiveEntryResult.Location}.";
                result.LogLevel = LogLevel.Error;
                result.Exception = ex;
            }

            return result;
        }
    }
}