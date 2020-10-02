using System;

namespace Simplic.Package
{
    public class InstallObjectResult : LogResult
    {
        public bool Success { get; set; }
        public Exception Exception { get; set; }
    }
}