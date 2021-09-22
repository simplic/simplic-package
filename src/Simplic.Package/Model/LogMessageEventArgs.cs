using System;

namespace Simplic.Package
{
    /// <summary>
    /// Arguments for a log message event.
    /// </summary>
    public class LogMessageEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the log message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the log level.
        /// <para>
        /// This will indicate the severity of the log entry.
        /// </para>
        /// </summary>
        public LogLevel LogLevel { get; set; }

        /// <summary>
        /// Gets or sets the exception.
        /// <para>
        /// Might only be used in case of an error or debug entry.
        /// </para>
        /// </summary>
        public Exception Exception { get; set; }
    }
}