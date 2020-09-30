using System;

namespace Simplic.Package
{
    public class UnpackObjectResult
    {
        public InstallableObject InstallableObject { get; set; }
        public string LogMessage { get; set; }
        public LogLevel LogLevel { get; set; }
        public Exception Exception { get; set; }
    }
}