using System.Threading.Tasks;

namespace Simplic.Package
{
    /// <summary>
    /// Service for unpacking (making them installable) objects
    /// </summary>
    public interface IUnpackObjectService
    {
        /// <summary>
        /// Unpacks an object and makes it installable
        /// </summary>
        /// <param name="extractArchiveEntryResult">The archive entry to unpack from</param>
        /// <returns>A UnpackObjectResult object that is wrapping the InstallableObject</returns>
        Task<UnpackObjectResult> UnpackObject(ExtractArchiveEntryResult extractArchiveEntryResult);
    }
}