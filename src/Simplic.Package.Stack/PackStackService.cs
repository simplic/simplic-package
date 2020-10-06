using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package.Stack
{
    public class PackStackService : PackObjectServiceBase, IPackObjectService
    {
        public PackStackService(IFileService fileService) : base(fileService)
        {
        }
    }
}
