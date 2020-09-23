using System;
using System.Collections.Generic;

namespace Simplic.Package
{
    // TODO: Rename something more fitting. e.g. PackageConfiguration
    public class Package
    {
        public string Name { get; set; }
        public Version Version { get; set; }
        public IList<Dependency> Dependencies { get; set; }
        public IDictionary<string, IList<ObjectListItem>> Objects { get; set; }
    }
}
