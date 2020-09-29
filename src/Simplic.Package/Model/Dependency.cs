using System;

namespace Simplic.Package
{
    public class Dependency
    {
        public string PackageName { get; set; }
        public Version Version { get; set; }
        public bool GreaterAllowed { get; set; }
    }
}