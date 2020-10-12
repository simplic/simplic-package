using System.Threading.Tasks;

namespace Simplic.Package
{
    /// <summary>
    /// Base PackObjectService implementation
    /// </summary>
    public abstract class PackObjectServiceBase : IPackObjectService
    {
        protected readonly IFileService fileService;
        public PackObjectServiceBase(IFileService fileService)
        {
            this.fileService = fileService;
        }

        /// <summary>
        /// Packs a ObjectListItem into a PackObjectResult
        /// </summary>
        /// <param name="item">The ObjectListItem</param>
        /// <returns>A PackObjectResult object</returns>
        public virtual async Task<PackObjectResult> ReadAsync(ObjectListItem item)
        {
            return new PackObjectResult
            {
                File = await fileService.ReadAllBytesAsync(item.Source),
                Location = item.Target,
            };
        }
    }
}
