namespace Simplic.Package.Report
{
    /// <summary>
    /// Represents the content of a full report.
    /// </summary>
    public class FullReport : IContent
    {
        /// <summary>
        /// Gets or sets the report.
        /// </summary>
        public Report Report { get; set; }

        /// <summary>
        /// Gets or sets the report data.
        /// </summary>
        public byte[] ReportData { get; set; }
    }
}