using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package
{
    public class UnpackedPackage
    {
        public string Name { get; set; }
        public Version Version { get; set; }
        public IList<Dependency> Dependencies { get; set; }
        public Dictionary<string, IList<UnpackObjectResult>> UnpackedObjects { get; set; }
    }
}
