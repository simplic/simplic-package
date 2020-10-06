using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package.StackContextArea
{
    public class PackStackContextAreaService : PackObjectServiceBase, IPackObjectService
    {
        public PackStackContextAreaService(IFileService fileService) : base(fileService)
        {
        }
    }
}
