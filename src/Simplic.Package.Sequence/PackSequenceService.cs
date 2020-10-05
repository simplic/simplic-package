using System.Threading.Tasks;

namespace Simplic.Package.Sequence
{
    public class PackSequenceService : IPackObjectService
    {
        private readonly IFileService fileService;

        public PackSequenceService(IFileService fileService)
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