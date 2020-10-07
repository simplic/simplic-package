using System.Threading.Tasks;

namespace Simplic.Package
{
    public interface IInitializePackageSystemService
    {
        Task<InitializePackageSystemResult> Initialize();
    }
}