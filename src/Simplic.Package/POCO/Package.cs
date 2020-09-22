using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package.POCO
{
    public class Package
    {
        public string Name { get; set; }
        public Version Version { get; set; }
        public IList<Dependency> Dependencies { get; set; }
        public Dictionary<String, Object> Objects { get; set; }
    }
}
