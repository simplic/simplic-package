using System.Threading.Tasks;

namespace Simplic.Package.Icon
{
    internal class PackIconService : IPackObjectService
    {
        private readonly IFileService fileService;

        public PackIconService(IFileService fileService)
        {
            this.fileService = fileService;
        }

        public async Task<PackObjectResult> ReadAsync(ObjectListItem item)
        {
            return new PackObjectResult
            {
                File = await fileService.ReadAllBytesAsync(item.Source),
                Location = item.Target
            };
        }
    }
}