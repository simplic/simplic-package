using System;
using System.Collections.Generic;

namespace Simplic.Package
{
    public class PackageConfiguration
    {
        public Version PackageFormatVersion { get; set; }
        public string Name { get; set; }
        public Version Version { get; set; }
        public IList<Dependency> Dependencies { get; set; }
        public IDictionary<string, IList<ObjectListItem>> Objects { get; set; }
    }
}