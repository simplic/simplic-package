using System.Collections.Generic;

namespace Simplic.Package.Report
{
    /// <summary>
    /// Represents the content of a key value report configuration.
    /// </summary>
    public class KeyValueConfiguration : ReportConfiguration
    {
        /// <summary>
        /// Gets or sets the is list based flag.
        /// </summary>
        public bool IsListBased { get; set; }

        /// <summary>
        /// Gets or sets the parameter.
        /// </summary>
        public IList<KeyValueParameterItem> Parameter { get; set; } = new List<KeyValueParameterItem>();
    }
}