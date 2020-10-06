using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package.StackRegister
{
    public class StackRegister : IContent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid StackId { get; set; }
        public bool IsActive { get; set; }
        public string Type { get; set; }
        public IStackRegisterConfiguration Configuration { get; set; }
    }
}
