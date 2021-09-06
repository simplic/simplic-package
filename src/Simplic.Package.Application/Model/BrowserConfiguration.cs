namespace Simplic.Package.Application
{
    /// <summary>
    /// Represents the application configuration for browser applications.
    /// </summary>
    public class BrowserConfiguration : IApplicationConfiguration
    {
        /// <summary>
        /// Gets or sets the url.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the tab.
        /// </summary>
        public string Tab { get; set; }
    }
}