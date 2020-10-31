using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package
{
    /// <summary>
    /// Base UnpackObjectService implementation
    /// Unpacks a Json Object by deserializing it into a given Type
    /// </summary>
    /// <typeparam name="T">The type</typeparam>
    public abstract class UnpackObjectServiceBase<T> : IUnpackObjectService where T : class, IContent
    {
        /// <summary>
        /// Unpacks a Json Object by deserializing it
        /// </summary>
        /// <param name="extractArchiveEntryResult">The archive entry containing the json file</param>
        /// <returns>A UnpackObjectResult object</returns>
        public async Task<UnpackObjectResult> UnpackObject(ExtractArchiveEntryResult extractArchiveEntryResult)
        {
            var result = new UnpackObjectResult
            {
                LogLevel = LogLevel.Info
            };
                        
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
