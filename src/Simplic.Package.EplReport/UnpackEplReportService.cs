using Newtonsoft.Json.Linq;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package.EplReport
{
    public class UnpackEplReportService : IUnpackObjectService
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

                var configuration = jObject["configuration"];
                jObject.Remove("configuration");

                var content = jObject.ToObject<EplReport>();
                content.Configuration = DeserializeConfiguration(content.Type, configuration);

                result.InstallableObject = new InstallableObject
                {
                    Content = content,
                    Target = extractArchiveEntryResult.Location,
                    Mode = extractArchiveEntryResult.Mode
                };
                result.Message = $"Unpacked EplReport at {extractArchiveEntryResult.Location}.";
            }
            catch (Exception ex)
            {
                result.Message = $"Failed to unpack EplReport at {extractArchiveEntryResult.Location}.";
                result.LogLevel = LogLevel.Error;
                result.Exception = ex;
            }
            return result;
        }

        private IEplReportConfiguration DeserializeConfiguration(string type, JToken configuration)
        {
            if (type == "sequence")
                return configuration.ToObject<SequenceConfiguration>();
            else if (type == "sql")
                return configuration.ToObject<SqlConfiguration>();
            else if (type == "grid")
                return configuration.ToObject<GridConfiguration>();
            return null;
        }
    }
}