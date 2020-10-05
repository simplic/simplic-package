using System.Threading.Tasks;

namespace Simplic.Package.ItemBox
{
    public class PackItemBoxService : IPackObjectService
    {
        public readonly IFileService fileService;

        public PackItemBoxService(IFileService fileService)
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