using System.Threading.Tasks;

namespace Simplic.Package
{
    /// <summary>
    /// Service for packing objects
    /// </summary>
    public interface IPackObjectService
    {
        /// <summary>
        /// Reads in the content of an items source file
        /// </summary>
        /// <param name="item">The item whos file to read the contents from</param>
        /// <returns>A PackObjectResult object</returns>
        Task<PackObjectResult> ReadAsync(ObjectListItem item);
    }
}