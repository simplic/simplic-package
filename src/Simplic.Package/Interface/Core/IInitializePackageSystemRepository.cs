using System.Threading.Tasks;

namespace Simplic.Package
{
    /// <summary>
    /// Repository for initializiting the Package System.
    /// </summary>
    public interface IInitializePackageSystemRepository
    {
        /// <summary>
        /// Initializes the Package System by creating the tables necesarry for having package version control.
        /// </summary>
        /// <returns>A <see cref="InitializePackageSystemResult"/> object.</returns>
        Task<InitializePackageSystemResult> Initialize();
    }
}