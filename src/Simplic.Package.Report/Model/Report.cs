using Newtonsoft.Json;
using System;

namespace Simplic.Package.Report
{
    /// <summary>
    /// Represents the content of a report.
    /// </summary>
    public class Report
    {
        /// <summary>
        /// Gets or sets the id.
        /// <para>
        /// Represents the unique identifier and primary key of a report.
        /// </para>
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the report name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the report type.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the printer name.
        /// </summary>
        public string PrinterName { get; set; }

        /// <summary>
        /// Gets or sets the report configuration.
        /// </summary>
        public ReportConfiguration Configuration { get; set; }
    }
}