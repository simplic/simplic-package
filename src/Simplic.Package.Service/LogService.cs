using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package.Service
{
    public class LogService : ILogService
    {
        public event EventHandler<LogMessageEventArgs> MessageAdded;
        public Protocol Protocol { get; set; } = new Protocol();

        public async Task WriteAsync(string message, LogLevel logLevel)
        {
            MessageAdded?.Invoke(this, new LogMessageEventArgs { Message = message, LogLevel = logLevel });

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
