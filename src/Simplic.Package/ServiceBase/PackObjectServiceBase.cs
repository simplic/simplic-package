using System.Threading.Tasks;

namespace Simplic.Package
{
    public abstract class PackObjectServiceBase : IPackObjectService
    {
        protected readonly IFileService fileService;
        public PackObjectServiceBase(IFileService fileService)
        {
            this.fileService = fileService;
        }

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
