using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package.Application
{
    public class UnpackApplicationService : IUnpackObjectService
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
                var settingsJson = jObject["configuration"].ToString();
                jObject.Remove("configuration");

                // Seperately deserialize settings and rest of json
                var deserializedApplication = jObject.ToObject<DeserializedApplication>();
                deserializedApplication.Configuration = DeseralizeSettings(deserializedApplication.Type, settingsJson);

                result.InstallableObject = new InstallableObject
                {
                    Content = deserializedApplication,
                    Target = extractArchiveEntryResult.Location,
                    Mode = extractArchiveEntryResult.Mode
                };
                result.Message = $"Unpacked Application at {extractArchiveEntryResult.Location}.";
            }
            catch (Exception ex)
            {
                result.Message = $"Failed to unpack Application at {extractArchiveEntryResult.Location}.";
                result.LogLevel = LogLevel.Error;
                result.Exception = ex;
            }
            return result;
        }

        private IApplicationConfiguration DeseralizeSettings(string type, string settingsJson)
        {
            if (type == "grid")
                return JsonConvert.DeserializeObject<GridConfiguration>(settingsJson);
            else if (type == "grid-structure")
                return JsonConvert.DeserializeObject<GridStructureConfiguration>(settingsJson);
            else if (type == "browser")
                return JsonConvert.DeserializeObject<BrowserConfiguration>(settingsJson);
            else if (type == "python")
                return JsonConvert.DeserializeObject<PythonConfiguration>(settingsJson);
            else if (type == "clr")
                return JsonConvert.DeserializeObject<ClrConfiguration>(settingsJson);
            return null;
        }
    }
}