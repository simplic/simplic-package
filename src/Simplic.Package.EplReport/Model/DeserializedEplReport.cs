using System;

namespace Simplic.Package.EplReport
{
    public class DeserializedEplReport : IContent
    {
        public Guid Id { get; set; }
        public string InternalName { get; set; }
        public string ReportDesignId { get; set; }
        public string Printer { get; set; }
        public bool IsContextlessPrintable { get; set; }
        public string Type { get; set; }
        public IEplReportConfiguration Configuration { get; set; }
    }
}