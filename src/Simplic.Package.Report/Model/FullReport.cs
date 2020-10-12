namespace Simplic.Package.Report
{
    public class FullReport : IContent
    {
        public Report Report { get; set; }
        public byte[] ReportData { get; set; }
    }
}