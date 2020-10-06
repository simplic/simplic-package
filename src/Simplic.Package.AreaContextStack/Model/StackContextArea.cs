using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package.StackContextArea
{
    public class StackContextArea : IContent
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        public string Type { get; set; }
        public IStackContextAreaConfiguration Configuration {get;set;}
    }
}
