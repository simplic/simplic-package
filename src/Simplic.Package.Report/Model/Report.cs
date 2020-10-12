using Newtonsoft.Json;
using System;

namespace Simplic.Package.Report
{
    public class Report
    {
        [JsonProperty(Required = Required.Always)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string ReportFile { get; set; } // TODO: Is this report name? https://simplic.github.io/dev/api_core/api/Simplic.Reporting.IReportConfiguration.html#Simplic_Reporting_IReportConfiguration_Report
        public ReportConfiguration Configuration { get; set; }
    }
}