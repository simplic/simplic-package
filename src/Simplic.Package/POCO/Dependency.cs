using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package.POCO
{
    public class Dependency
    {
        public string Package { get; set; }
        public Version Version { get; set; }
        public bool GreaterAllowed { get; set; }
    }
}
