using System.Threading.Tasks;

namespace Simplic.Package.EplReport
{
    public class PackEplReportService : IPackObjectService
    {
        private readonly IFileService fileService;

        public PackEplReportService(IFileService fileService)
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