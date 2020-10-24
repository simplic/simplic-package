using System;

namespace Simplic.Package
{
    /// <summary>
    /// Represets a log item
    /// </summary>
    public class ProtocolItem
    {
        /// <summary>
        /// Gets or sets the log-datetime
        /// </summary>
        public DateTime DateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// Gets or sets the message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the log level
        /// </summary>
        public LogLevel LogLevel { get; set; }

        /// <summary>
        /// Gets or sets the associated exception
        /// </summary>
        public Exception Exception { get; set; }
    }
}
