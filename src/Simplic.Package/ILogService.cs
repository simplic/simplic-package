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
        /// <returns></returns>
        Task WriteAsync(string message, LogLevel logLevel);
    }
}