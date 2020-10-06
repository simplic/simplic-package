using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package.EplReportDesign
{
    public class PackEplReportDesignService : PackObjectServiceBase, IPackObjectService
    {
        public PackEplReportDesignService(IFileService fileService) : base(fileService)
        {
        }
    }
}
