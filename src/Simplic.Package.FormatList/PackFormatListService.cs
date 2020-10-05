using System.Threading.Tasks;

namespace Simplic.Package.FormatList
{
    public class PackFormatListService : IPackObjectService
    {
        private readonly IFileService fileService;

        public PackFormatListService(IFileService fileService)
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