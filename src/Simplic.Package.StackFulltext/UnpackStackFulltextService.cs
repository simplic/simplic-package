using Newtonsoft.Json.Linq;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package.StackFulltext
{
    /// <summary>
    /// Service to unpack a stac fulltext.
    /// </summary>
    public class UnpackStackFulltextService : IUnpackObjectService
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
                var jObject = JObject.Parse(json);

                // Seperate settings and rest of json
                var configuration = jObject["Configuration"];
                jObject.Remove("Configuration");

                // Seperately deserialize settings and rest of json
                var deserializedStackFulltext = jObject.ToObject<StackFulltext>();
                deserializedStackFulltext.Configuration = DeserializedConfiguration(deserializedStackFulltext.Type, configuration);

                result.InstallableObject = new InstallableObject
                {
                    Content = deserializedStackFulltext,
                    Target = extractArchiveEntryResult.Location,
                    Mode = extractArchiveEntryResult.Mode
                };
                result.Message = $"Unpacked StackContextArea at {extractArchiveEntryResult.Location}.";
            }
            catch (Exception ex)
            {
                result.Message = $"Failed to unpack Fulltext at {extractArchiveEntryResult.Location}.";
                result.LogLevel = LogLevel.Error;
                result.Exception = ex;
            }
            return result;
        }

        private IStackFulltextConfiguration DeserializedConfiguration(string type, JToken configuration)
        {
            if (type == "sql")
                return configuration.ToObject<SqlConfiguration>();
            return null;
        }
    }
}