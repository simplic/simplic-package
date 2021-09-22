namespace Simplic.Package.EplReport
{
    /// <summary>
    /// Represents the sql cofiguration of an epl report configuration.
    /// </summary>
    public class SqlConfiguration : IEplReportConfiguration
    {
        /// <summary>
        /// Gets or sets the sql data source code.
        /// </summary>
        public string SqlDataSourceCode { get; set; }
    }
}