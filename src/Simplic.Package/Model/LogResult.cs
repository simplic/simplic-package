using System;

namespace Simplic.Package
{
    public abstract class LogResult
    {
        public string LogMessage { get; set; }
        public LogLevel LogLevel { get; set; }
        public Exception Exception { get; set; }
    }
}