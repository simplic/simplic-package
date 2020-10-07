using System;
using System.Threading.Tasks;

namespace Simplic.Package.Service
{
    public class LogService : ILogService

    {       /// <summary>
            /// Subscribers to this event are invoked whenever a message is added to the log
            /// </summary>
        public event EventHandler<LogMessageEventArgs> MessageAdded;

        public Protocol Protocol { get; set; } = new Protocol();

        /// <summary>
        /// Adds a message to the log
        /// </summary>
        /// <param name="message">The message to add</param>
        /// <param name="logLevel">The LogLevel of the message, indicating its severity</param>
        /// <param name="ex">The caught exception</param>
        /// <returns></returns>
        public async Task WriteAsync(string message, LogLevel logLevel, Exception exception = null)
        {
            // TODO: Potentially add the time here already if its needed in the log
            MessageAdded?.Invoke(this, new LogMessageEventArgs { Message = message, LogLevel = logLevel, Exception = exception });

            if (logLevel == LogLevel.Error)
                Protocol.Error.Add(message);
            else if (logLevel == LogLevel.Warning)
                Protocol.Warning.Add(message);
            else if (logLevel == LogLevel.Info)
                Protocol.Warning.Add(message);
            else if (logLevel == LogLevel.Debug)
                Protocol.Debug.Add(message);
        }
    }
}