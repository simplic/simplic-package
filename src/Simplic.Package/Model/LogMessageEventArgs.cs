using System;

namespace Simplic.Package
{
    public class LogMessageEventArgs : EventArgs
    {
        public string Message { get; set; }
        public LogLevel LogLevel { get; set; }
    }
}