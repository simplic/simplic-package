using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package
{
    public abstract class UnpackObjectServiceBase<T> : IUnpackObjectService where T : class, IContent
    {
        public async Task<UnpackObjectResult> UnpackObject(ExtractArchiveEntryResult extractArchiveEntryResult)
        {
            var result = new UnpackObjectResult
            {
                LogLevel = LogLevel.Info
            };

            Debugger.Launch();
            try
            {
                var json = Encoding.Default.GetString(extractArchiveEntryResult.Data);
                var content = JsonConvert.DeserializeObject<T>(json);

                result.InstallableObject = new InstallableObject
                {
                    Content = content,
                    Target = extractArchiveEntryResult.Location,
                    Mode = extractArchiveEntryResult.Mode
                };
                result.Message = $"Unpacked {ElementName} at {extractArchiveEntryResult.Location}.";
            }
            catch (Exception ex)
            {
                result.Message = $"Failed to unpack {ElementName} at {extractArchiveEntryResult.Location}.";
                result.LogLevel = LogLevel.Error;
                result.Exception = ex;
            }
            return result;
        }

        private string ElementName => typeof(T).Name.Replace("Unpack", "");
    }
}
