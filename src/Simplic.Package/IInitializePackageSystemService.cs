using System.Threading.Tasks;

namespace Simplic.Package
{
    /// <summary>
    /// Service for initializiting the Package System
    /// </summary>
    public interface IInitializePackageSystemService
    {
        /// <summary>
        /// Initializes the Package System by creating the tables necesarry for having package version control
        /// </summary>
        Task Initialize();
    }
}