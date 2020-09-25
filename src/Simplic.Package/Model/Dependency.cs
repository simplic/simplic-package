using System;

namespace Simplic.Package
{
    public class Dependency
    {
        public string Package { get; set; }
        public Version Version { get; set; }
        public bool GreaterAllowed { get; set; }
    }
}