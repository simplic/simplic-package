using System.Threading.Tasks;

namespace Simplic.Package
{
    /// <summary>
    /// Service for creating a package
    /// </summary>
    public interface IPackService
    {
        /// <summary>
        /// Creates and writes a package based on the package configuration file
        /// </summary>
        /// <param name="json">The package configuration file as a string</param>
        /// <returns>The written package in bytes</returns>
        Task<byte[]> Pack(string json);

        /// <summary>
        /// Creates and writes a package based on the package configuration file
        /// </summary>
        /// <param name="package">The deserialized configuration file</param>
        /// <returns>The written package in bytes</returns>
        Task<byte[]> Pack(Package package);
    }
}
