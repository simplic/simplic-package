using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package.StackFulltext
{
    public class PackStackFulltextService : PackObjectServiceBase, IPackObjectService
    {
        public PackStackFulltextService(IFileService fileService) : base(fileService)
        {
        }
    }
}
