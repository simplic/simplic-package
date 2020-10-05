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
                var settingsJson = jObject["settings"].ToString();
                jObject.Remove("settings");

                // Seperately deserialize settings and rest of json
                var deserializedApplication = jObject.ToObject<DeserializedApplication>();
                deserializedApplication.Settings = DeseralizeSettings(deserializedApplication.Type, settingsJson);

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

        private IApplicationSettings DeseralizeSettings(string type, string settingsJson)
        {
            if (type == "grid")
                return JsonConvert.DeserializeObject<GridSettings>(settingsJson);
            else if (type == "grid_structure")
                return JsonConvert.DeserializeObject<GridStructureSettings>(settingsJson);
            else if (type == "browser")
                return JsonConvert.DeserializeObject<BrowserSettings>(settingsJson);
            else if (type == "python")
                return JsonConvert.DeserializeObject<PythonSettings>(settingsJson);
            else if (type == "clr")
                return JsonConvert.DeserializeObject<ClrSettings>(settingsJson);
            return null;
        }
    }
}