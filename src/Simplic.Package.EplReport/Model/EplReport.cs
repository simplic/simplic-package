using Newtonsoft.Json;
using System;

namespace Simplic.Package.EplReport
{
    /// <summary>
    /// Represents the content of an epl report.
    /// </summary>
    public class EplReport : IContent
    {
        /// <summary>
        /// Gets or sets the id.
        /// <para>
        /// Represents the unique identifier and primary key of an epl report.
        /// </para>
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the internal name.
        /// </summary>
        public string InternalName { get; set; }

        /// <summary>
        /// Gets or sets the report desing id.
        /// </summary>
        public string ReportDesignId { get; set; }

        /// <summary>
        /// Gets or sets the printer.
        /// </summary>
        public string Printer { get; set; }

        /// <summary>
        /// Gets or sets the flag whether the report is contextless printable.
        /// </summary>
        public bool IsContextlessPrintable { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the configuration.
        /// </summary>
        public IEplReportConfiguration Configuration { get; set; }
    }
}