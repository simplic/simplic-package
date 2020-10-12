using Newtonsoft.Json.Linq;
using Simplic.Package.StackAutoconnector.Model;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package.StackAutoconnector
{
    public class UnpackStackAutoconnectorService : IUnpackObjectService
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
                var settingsJson = jObject["configuration"];
                jObject.Remove("configuration");

                // Seperately deserialize settings and rest of json
                var deserializedAutoStackonnector = jObject.ToObject<StackAutoconnector>();
                deserializedAutoStackonnector.Configuration = DeserializedConfiguration(deserializedAutoStackonnector.Type, settingsJson);

                result.InstallableObject = new InstallableObject
                {
                    Content = deserializedAutoStackonnector,
                    Target = extractArchiveEntryResult.Location,
                    Mode = extractArchiveEntryResult.Mode
                };
                result.Message = $"Unpacked StackAutoconnctor at {extractArchiveEntryResult.Location}.";
            }
            catch (Exception ex)
            {
                result.Message = $"Failed to unpack StackAutoconnctor at {extractArchiveEntryResult.Location}.";
                result.LogLevel = LogLevel.Error;
                result.Exception = ex;
            }
            return result;
        }

        private IStackAutoconnectorConfiguration DeserializedConfiguration(string type, JToken settingsJson)
        {
            if (type == "xml")
                return settingsJson.ToObject<XmlConfiguration>();
            return null;
        }
    }
}