using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package
{
    // TODO: Think of better name. 
    //This wraps the content of an unpacked ObjectListItem (e.g. a deserialized Grid) with information for installing it
    public class InstallableObject
    {
        public string Location { get; set; }
        public IContent Content { get; set; }
    }
}
