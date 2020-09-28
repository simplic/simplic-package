using System;
using System.Collections.Generic;

namespace Simplic.Package
{
    public class Package
    {
        public string Name { get; set; }
        public Version Version { get; set; }
        public IList<Dependency> Dependencies { get; set; }
        public Dictionary<string, IList<InstallableObject>> UnpackedObjects { get; set; }
    }
}