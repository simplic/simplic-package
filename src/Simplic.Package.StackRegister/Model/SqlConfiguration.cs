using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package.StackRegister
{
    class SqlConfiguration : IStackRegisterConfiguration
    {
        public string SqlStatement { get; set; }
    }
}
