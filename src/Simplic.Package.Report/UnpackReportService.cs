using Newtonsoft.Json.Linq;
using Simplic.Package.Report.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package.Report
{
    public class UnpackReportService : IUnpackObjectService
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

                var content = jObject.ToObject<DeserializedReport>();
                content.Configuration = DeserializeConfiguration(content.Type, configuration);

                result.InstallableObject = new InstallableObject
                {
                    Content = content,
                    Target = extractArchiveEntryResult.Location,
                    Mode = extractArchiveEntryResult.Mode
                };
                result.Message = $"Unpacked Report at {extractArchiveEntryResult.Location}.";
            }
            catch (Exception ex)
            {
                result.Message = $"Failed to unpack Report at {extractArchiveEntryResult.Location}.";
                result.LogLevel = LogLevel.Error;
                result.Exception = ex;
            }
            return result;
        }

        private ReportConfiguration DeserializeConfiguration(string type, JToken configuration)
        {
            if (type == "sql")
                return configuration.ToObject<SqlConfiguration>();
            else if (type == "key-value")
                return configuration.ToObject<KeyValueConfiguration>();
            else if (type == "parameter")
                return configuration.ToObject<ParameterConfiguration>();
            return null;
        }
    }
}
