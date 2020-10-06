using System.Threading.Tasks;

namespace Simplic.Package.ComboBox
{
    public class PackComboBoxService : PackObjectServiceBase, IPackObjectService
    {
        public PackComboBoxService(IFileService fileService) : base(fileService)
        {
        }
    }
}