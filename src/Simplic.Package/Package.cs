using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package
{
    public class Package
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public List<Dictionary<String, String>> Dependencies { get; set; }
        public Dictionary<String, Dictionary<String, Dictionary<String, String>>> Objects { get; set; }
    }
}
