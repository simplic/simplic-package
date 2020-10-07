using System;
using System.Threading.Tasks;

namespace Simplic.Package
{
    /// <summary>
    /// Service to check and add package versions in the database
    /// </summary>
    public interface IPackageTrackingRepository
    {
        /// <summary>
        /// Gets the installed version of a given package
        /// </summary>
        /// <param name="guid">The guid of the package</param>
        /// <returns>The version or null if no version exists</returns>
        Task<Version> GetPackageVersion(Guid guid);

        /// <summary>
        /// Gets the installed version of a given package
        /// </summary>
        /// <param name="packageName">The name of the package</param>
        /// <returns>The version or null if no version exists</returns>
        Task<Version> GetPackageVersion(string packageName);

        /// <summary>
        /// Adds the version of a given package to the database
        /// </summary>
        /// <param name="package">The package whos version is to add</param>
        /// <returns>Whether adding the version worked</returns>
        Task<bool> AddPackgageVersion(Package package);

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