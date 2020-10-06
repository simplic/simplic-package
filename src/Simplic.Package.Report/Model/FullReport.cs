namespace Simplic.Package.Report
{
    public class FullReport : IContent
    {
        public DeserializedReport Report { get; set; }
        public byte[] ReportData { get; set; }
    }
}
