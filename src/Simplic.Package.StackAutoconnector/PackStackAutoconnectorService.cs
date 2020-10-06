using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package.StackAutoconnector
{
    public class PackStackAutoconnectorService : PackObjectServiceBase, IPackObjectService
    {
        public PackStackAutoconnectorService(IFileService fileService) : base(fileService)
        {
        }
    }
}
