using System.Threading.Tasks;

namespace Simplic.Package
{
    public interface IUnpackObjectService
    {
        Task<UnpackObjectResult> UnpackObject(ExtractArchiveEntryResult extractArchiveEntryResult);
    }
}