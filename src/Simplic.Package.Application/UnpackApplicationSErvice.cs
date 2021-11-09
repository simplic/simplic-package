using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package.Application
{
    /// <summary>
    /// Service to unpack applications.
    /// </summary>
    public class UnpackApplicationService : IUnpackObjectService
    {
        /// <inheritdoc/>
#pragma warning disable 1998
        public async Task<UnpackObjectResult> UnpackObject(ExtractArchiveEntryResult extractArchiveEntryResult)
#pragma warning restore 1998
        {
            var result = new UnpackObjectResult
            {
                LogLevel = LogLevel.Info
            };

            try
            {
                var json = Encoding.UTF8.GetString(extractArchiveEntryResult.Data);
                var jObject = JObject.Parse(json);

                // Separate settings and rest of json
                var settingsJson = jObject["Configuration"]?.ToString();
                jObject.Remove("Configuration");

                // Separately deserialize settings and rest of json
                var deserializedApplication = jObject.ToObject<Application>();

                if (deserializedApplication == null)
                    throw new NullReferenceException("Deserialized application is null");


                deserializedApplication.Configuration =
                    DeserializeSettings(deserializedApplication.Type, settingsJson);

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

        /// <summary>
        /// Deserializes application configuration.
        /// </summary>
        /// <param name="type">The type name.</param>
        /// <param name="settingsJson">The json string containing the configuration.</param>
        /// <returns>Returns the application configuration.</returns>
        private static IApplicationConfiguration DeserializeSettings(string type, string settingsJson)
        {
            switch (type)
            {
                case "grid":
                    return JsonConvert.DeserializeObject<GridConfiguration>(settingsJson);
                case "grid-structure":
                    return JsonConvert.DeserializeObject<GridStructureConfiguration>(settingsJson);
                case "browser":
                    return JsonConvert.DeserializeObject<BrowserConfiguration>(settingsJson);
                case "python":
                    return JsonConvert.DeserializeObject<PythonConfiguration>(settingsJson);
                case "clr":
                    return JsonConvert.DeserializeObject<ClrConfiguration>(settingsJson);
                default:
                    return null;
            }
        }
    }
}