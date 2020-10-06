using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package.StackFulltext
{
    public class SqlConfiguration : IStackFulltextConfiguration
    {
        public string Statement { get; set; } // TODO: This is not uniform in the sample
    }
}
