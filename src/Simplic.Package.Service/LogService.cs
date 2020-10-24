using System;
using System.Threading.Tasks;

namespace Simplic.Package.Service
{
    /// <summary>
    /// Log service implementation
    /// </summary>
    public class LogService : ILogService
    {
        /// <summary>
        /// Subscribers to this event are invoked whenever a message is added to the log
        /// </summary>
        public event EventHandler<LogMessageEventArgs> MessageAdded;

        /// <summary>
        /// Adds a message to the log
        /// </summary>
        /// <param name="message">The message to add</param>
        /// <param name="logLevel">The LogLevel of the message, indicating its severity</param>
        /// <param name="ex">The caught exception</param>
        /// <returns></returns>
        public async Task WriteAsync(string message, LogLevel logLevel, Exception exception = null)
        {
            MessageAdded?.Invoke(this, new LogMessageEventArgs { Message = message, LogLevel = logLevel, Exception = exception });

            Protocol.Messages.Add(new ProtocolItem
            {
                Message = message,
                LogLevel = logLevel,
                Exception = exception
            });
        }

        /// <summary>
        /// Gets or sets the actual log protocol instance
        /// </summary>
        public Protocol Protocol { get; set; } = new Protocol();
    }
}