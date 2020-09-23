using System.Threading.Tasks;

namespace Simplic.Package
{
    public interface IPackObjectService
    {
        Task<PackObjectResult> ReadAsync(ObjectListItem item);
    }
}
