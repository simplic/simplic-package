using System.Threading.Tasks;

namespace Simplic.Package
{
    /// <summary>
    /// Service for unpacking packages.
    /// </summary>
    public interface IUnpackService
    {
        /// <summary>
        /// Unpacks the contents of a given package.
        /// </summary>
        /// <param name="packagePath">The path to the package.</param>
        /// <returns>A <see cref="Package"/> object.</returns>
        Task<Package> Unpack(string packagePath);

        /// <summary>
        /// Unpacks the contents of a given package.
        /// </summary>
        /// <param name="packageBytes">The package as bytes.</param>
        /// <returns>A <see cref="Package"/> object.</returns>
        Task<Package> Unpack(byte[] packageBytes);
    }
}