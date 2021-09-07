using System;

namespace Simplic.Package.EplReport
{
    /// <summary>
    /// Represents a sequece configuration of an epl report.
    /// </summary>
    public class SequenceConfiguration : IEplReportConfiguration
    {
        /// <summary>
        /// Gets or sets the sequence id.
        /// </summary>
        public Guid SequenceId { get; set; }
    }
}