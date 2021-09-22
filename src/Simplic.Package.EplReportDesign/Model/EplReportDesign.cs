using Newtonsoft.Json;
using System;

namespace Simplic.Package.EplReportDesign
{
    /// <summary>
    /// Represents the package content of an epl report design.
    /// </summary>
    public class EplReportDesign : IContent
    {
        /// <summary>
        /// Gets or sets the id.
        /// <para>
        /// Represents the unique identifier and primary key.
        /// </para>
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the internal name.
        /// </summary>
        public string InternalName { get; set; }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the report content.
        /// </summary>
        public string ReportContent { get; set; }

        /// <summary>
        /// Gets or sets the printer head width.
        /// </summary>
        public int PrinterHeadWidth { get; set; }
    }
}