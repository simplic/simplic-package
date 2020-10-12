using System;
using System.Threading.Tasks;

namespace Simplic.Package
{
    /// <summary>
    /// Service for logging Messages
    /// </summary>
    public interface ILogService
    {
        /// <summary>
        /// Subscribers to this event are invoked whenever a message is added to the log
        /// </summary>
        event EventHandler<LogMessageEventArgs> MessageAdded;

        /// <summary>
        /// Adds a message to the log
        /// </summary>
        /// <param name="message">The message to add</param>
        /// <param name="logLevel">The LogLevel of the message, indicating its severity</param>
        /// <param name="exception">The Exception that was caught</param>
        Task WriteAsync(string message, LogLevel logLevel, Exception exception = null);

        /// <summary>
        /// Adds a success Message to the log
        /// </summary>
        /// <param name="objectName">The name of the object</param>
        /// <param name="target">The path to the object inside the archive</param>
        Task WriteAsyncSuccess(string objectName, string target);

        /// <summary>
        /// Adds a fail Message to the log
        /// </summary>
        /// <param name="objectName">The name of the object</param>
        /// <param name="target">The path to the object inside the archive</param>
        Task WriteAsyncFailed(string objectName, string target);
    }
}