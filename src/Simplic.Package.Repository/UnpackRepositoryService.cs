using System;
using System.Threading.Tasks;

namespace Simplic.Package.Repository
{
    public class UnpackRepositoryService : IUnpackObjectService
    {
        Task<UnpackObjectResult> IUnpackObjectService.UnpackObject(ExtractArchiveEntryResult extractArchiveEntryResult)
        {
            throw new NotImplementedException();
        }
    }
}