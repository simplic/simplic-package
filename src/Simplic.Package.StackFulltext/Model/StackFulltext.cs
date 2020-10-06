using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package.StackFulltext
{
    public class StackFulltext : IContent
    {
        public Guid Id { get; set; }
        public Guid StackId { get; set; }
        public string Type { get; set; }
        public IStackFulltextConfiguration Configuration { get; set; }
    }
}
