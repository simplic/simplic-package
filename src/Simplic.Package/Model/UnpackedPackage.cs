using System;
using System.Collections.Generic;

namespace Simplic.Package
{
    public class UnpackedPackage
    {
        public string Name { get; set; }
        public Version Version { get; set; }
        public IList<Dependency> Dependencies { get; set; }
        public Dictionary<string, IList<InstallableObject>> UnpackedObjects { get; set; }
    }
}