using Newtonsoft.Json.Linq;
using Simplic.Package.Report.Model;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package.Report
{
    /// <summary>
    /// Service to unpack a report.
    /// </summary>
    public class UnpackReportService : IUnpackObjectService
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
                var fullReport = new FullReport();

                var json = Encoding.Default.GetString(extractArchiveEntryResult.Data);
                var jObject = JObject.Parse(json);

                var configuration = jObject["configuration"];
                jObject.Remove("configuration");

                var reportConfiguration = jObject.ToObject<Report>();
                reportConfiguration.Configuration = DeserializeConfiguration(reportConfiguration.Type, configuration);

                fullReport.Report = reportConfiguration;

                if (extractArchiveEntryResult.Payload.Any())
                    fullReport.ReportData = extractArchiveEntryResult.Payload.First().Value;

                result.InstallableObject = new InstallableObject
                {
                    Content = fullReport,
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