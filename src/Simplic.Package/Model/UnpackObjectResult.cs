using System;

namespace Simplic.Package
{
    public class UnpackObjectResult : LogResult
    {
        public InstallableObject InstallableObject { get; set; }
        public Exception Exception { get; set; }
    }
}