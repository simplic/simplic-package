using System.Threading.Tasks;

namespace Simplic.Package.Service
{
    public class PackGridService : IPackObjectService
    {
        private readonly FileService fileService;
        public PackGridService(FileService fileService)
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
