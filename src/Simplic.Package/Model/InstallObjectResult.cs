using System;

namespace Simplic.Package
{
    public class InstallObjectResult
    {
        public bool Success { get; set; }
        public string LogMessage { get; set; }
        public LogLevel LogLevel { get; set; }
        public Exception Exception { get; set; }
    }
}