using System.Threading.Tasks;

namespace Simplic.Package
{
    /// <summary>
    /// Service for checking if all dependencies are satisfied
    /// </summary>
    public interface ICheckDependencyService
    {
        /// <summary>
        /// Checks if a given dependecy is satisfied or not
        /// </summary>
        /// <param name="dependency">The dependency to check</param>
        /// <returns>A CheckDependecyResult object</returns>
        Task<CheckDependencyResult> Check(Dependency dependency);
    }
}