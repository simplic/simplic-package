using System.Threading.Tasks;

namespace Simplic.Package
{
    /// <summary>
    /// Service for packing (creating) packages
    /// </summary>
    public interface IPackService
    {
        /// <summary>
        /// Creates and writes a package based on the package configuration file
        /// </summary>
        /// <param name="json">The package configuration file as a string</param>
        /// <returns>The written archive in bytes</returns>
        Task<byte[]> Pack(string json);

        /// <summary>
        /// Creates and writes a package based on the given PackageConfiguration object
        /// </summary>
        /// <param name="packageConfiguration">The PackageConfiguration to create from</param>
        /// <returns>The written archive in bytes</returns>
        Task<byte[]> Pack(PackageConfiguration packageConfiguration);
    }
}