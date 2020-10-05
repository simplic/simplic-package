using System.Threading.Tasks;

namespace Simplic.Package.Application
{
    public class PackApplicationService : IPackObjectService
    {
        private readonly IFileService fileService;

        public PackApplicationService(IFileService fileService)
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