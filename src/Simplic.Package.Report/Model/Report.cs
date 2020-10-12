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
        public string PrinterName { get; set; }
        public ReportConfiguration Configuration { get; set; }
    }
}