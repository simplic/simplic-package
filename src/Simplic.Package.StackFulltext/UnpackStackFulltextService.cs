using Newtonsoft.Json.Linq;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package.StackFulltext
{
    public class UnpackStackFulltextService : IUnpackObjectService
    {
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
                var configuration = jObject["configuration"];
                jObject.Remove("configuration");

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
                result.Message = $"Failed to unpack StackContextArea at {extractArchiveEntryResult.Location}.";
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