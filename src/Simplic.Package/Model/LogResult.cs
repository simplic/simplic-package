using System;

namespace Simplic.Package
{
    public abstract class LogResult
    {
        public string Message { get; set; }
        public LogLevel LogLevel { get; set; }
        public Exception Exception { get; set; }
    }
}