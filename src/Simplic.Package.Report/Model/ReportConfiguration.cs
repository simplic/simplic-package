namespace Simplic.Package.Report
{
    /// <summary>
    /// Represents the content of a report configuration.
    /// </summary>
    public abstract class ReportConfiguration
    {
        /// <summary>
        /// Gets or sets the connection.
        /// </summary>
        public string Connection { get; set; }

        /// <summary>
        /// Gets or sets the provider.
        /// </summary>
        public string Provider { get; set; }
    }
}