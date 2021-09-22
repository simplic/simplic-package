using System.Collections.Generic;

namespace Simplic.Package.Report
{
    /// <summary>
    /// Represents the content of a parameter report configuration.
    /// </summary>
    public class ParameterConfiguration : ReportConfiguration
    {
        /// <summary>
        /// Gets or sets the parameter.
        /// </summary>
        public IList<string> Parameter { get; set; } = new List<string>();
    }
}