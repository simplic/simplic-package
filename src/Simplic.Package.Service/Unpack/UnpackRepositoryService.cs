using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package.Service.Unpack
{
    public class UnpackRepositoryService : IUnpackObjectService
    {
        Task<UnpackObjectResult> IUnpackObjectService.UnpackObject(ExtractArchiveEntryResult extractArchiveEntryResult)
        {
            throw new NotImplementedException();
        }
    }
}
