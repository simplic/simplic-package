using System.Threading.Tasks;

namespace Simplic.Package.ComboBox
{
    public class PackComboBoxService : IPackObjectService
    {
        public readonly IFileService fileService;

        public PackComboBoxService(IFileService fileService)
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