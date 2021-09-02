using System;

namespace Simplic.Package
{
    /// <summary>
    /// Base class for several log results.
    /// <para>
    /// The primary use is to prepare a base result for several check methods.
    /// </para>
    /// </summary>
    public abstract class LogResult
    {
        /// <summary>
        /// Gets or sets the log message.
        /// <para>
        /// Represents a message which will contain information why the log result was created.
        /// </para>
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the log level
        /// <para>
        /// The LogLevel of the result, indicating its severity.
        /// </para>
        /// </summary>
        public LogLevel LogLevel { get; set; }

        /// <summary>
        /// Gets or sets the log result exception
        /// <para>
        /// The exception can be added to the result to give further information why the result was created and the log level is chosen.
        /// Might only be set in error or debug log results.
        /// </para>
        /// </summary>
        public Exception Exception { get; set; }
    }
}