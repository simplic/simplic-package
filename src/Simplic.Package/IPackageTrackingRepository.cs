using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Simplic.Package
{
    /// <summary>
    /// Service to check and add package versions in the database
    /// </summary>
    public interface IPackageTrackingRepository
    {
        /// <summary>
        /// Gets all installed versions of a given package from the database
        /// </summary>
        /// <param name="packageName">The name of the package</param>
        /// <returns>An Enumerable of the installed versions</returns>
        Task<IEnumerable<Version>> GetPackageVersions(string packageName);

        /// <summary>
        /// Gets the latest installed version of a given package
        /// </summary>
        /// <param name="packageName">The name of the package</param>
        /// <returns>The latest version or null if no version exists</returns>
        Task<Version> GetLatestPackageVersion(string packageName);

        /// <summary>
        /// Adds the version of a given package to the database
        /// </summary>
        /// <param name="packageName">The name of the package</param>
        /// <param name="guid">The guid of the package</param>
        /// <param name="version">The version of the package</param>
        /// <returns>Whether adding the version worked</returns>
        Task<bool> AddPackgageVersion(string packageName, Guid guid, Version version);
    }
}