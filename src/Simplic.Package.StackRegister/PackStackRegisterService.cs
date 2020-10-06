using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package.StackRegister
{
    public class PackStackRegisterService : PackObjectServiceBase, IPackObjectService
    {
        public PackStackRegisterService(IFileService fileService) : base(fileService)
        {
        }
    }
}
