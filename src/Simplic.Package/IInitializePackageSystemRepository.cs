using System.Threading.Tasks;

namespace Simplic.Package
{
    public interface IInitializePackageSystemRepository
    {
        Task<InitializePackageSystemResult> Intialize();
    }
}