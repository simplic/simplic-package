using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package.StackAutoconnector
{
    public class DeserializedStackAutoconnector : IContent
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public Guid StackId { get; set; }
        public string Target { get; set; }
        public IStackAutoconnectorConfiguration Configuration { get; set; }
    }
}
