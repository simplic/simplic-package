namespace Simplic.Package.Configuration
{
    /// <summary>
    /// Represents a configuration.
    /// </summary>
    public class Configuration : IContent
    {
        /// <summary>
        /// Gets or sets the name of the configuration.
        /// </summary>
        public string ConfigurationName { get; set; }

        /// <summary>
        /// Gets or sets the plug in name.
        /// </summary>
        public string PlugInName { get; set; }

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the value source.
        /// </summary>
        public ConfigurationValueSource ValueSource { get; set; }
    }
}
