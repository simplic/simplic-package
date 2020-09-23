using System.Threading.Tasks;

namespace Simplic.Package
{
    /// <summary>
    /// Service for unpacking and deserializing a package
    /// </summary>
    public interface IUnpackService
    {
        /// <summary>
        /// Unpacks and deserializes the contents of a given package
        /// </summary>
        /// <param name="packagePath">The path to the package</param>
        /// <returns>A UnpackedPackage object</returns>
        Task<UnpackedPackage> Unpack(string packagePath);

        /// <summary>
        /// Unpacks and deserializes the contents of a given package
        /// </summary>
        /// <param name="packageBytes">The package as bytes</param>
        /// <returns>A UnpackedPackage object</returns>
        Task<UnpackedPackage> Unpack(byte[] packageBytes);
    }
}
