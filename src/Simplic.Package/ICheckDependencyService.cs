using System.Collections.Generic;
using System.Threading.Tasks;

namespace Simplic.Package
{
    /// <summary>
    /// Service for checking if all dependencies are satisfied
    /// </summary>
    public interface ICheckDependencyService
    {
        /// <summary>
        /// Checks is all dependencies are satisfied
        /// </summary>
        /// <param name="dependencies">A list of the dependecies to check</param>
        /// <returns>A CheckDependenciesResult object</returns>
        Task<CheckDependenciesResult> CheckAllDependencies(IList<Dependency> dependencies);

        /// <summary>
        /// Checks if a given dependecy is satisfied or not
        /// </summary>
        /// <param name="dependency">The dependency to check</param>
        /// <returns>Whether the dependency is satisfied</returns>
        Task<bool> Check(Dependency dependency);
    }
}