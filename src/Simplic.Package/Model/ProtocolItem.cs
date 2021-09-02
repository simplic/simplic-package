using System;

namespace Simplic.Package
{
    /// <summary>
    /// Represets a log item
    /// </summary>
    public class ProtocolItem
    {
        /// <summary>
        /// Gets or sets the log-datetime.
        /// <para>
        /// The DateTime will indicate when the protocol item was created.
        /// </para>
        /// </summary>
        public DateTime DateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// Gets or sets the message.
        /// <para>
        /// Represents the message of the protocol item.
        /// </para>
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the log level.
        /// <para>
        /// The LogLevel of the message, indicating its severity.
        /// </para>
        /// </summary>
        public LogLevel LogLevel { get; set; }

        /// <summary>
        /// Gets or sets the associated exception.
        /// <para>
        /// Will contain the exception of the protocol item when an exception is given.
        /// This might only happen in Error or debug cases.
        /// </para>
        /// </summary>
        public Exception Exception { get; set; }
    }
}
