using System.Threading.Tasks;

namespace Simplic.Package.Service
{
    public class PackRepositoryService : IPackObjectService
    {
        private readonly IFileService fileService;

        public PackRepositoryService(IFileService fileService)
        {
            this.fileService = fileService;
        }

        public async Task<PackObjectResult> ReadAsync(ObjectListItem item)
        {
            return new PackObjectResult
            {
                File = await fileService.ReadAllBytesAsync(item.Source),
                Location = $"repository/{item.Target}"
            };
        }
    }
}