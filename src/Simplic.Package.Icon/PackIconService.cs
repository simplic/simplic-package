using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Simplic.Package.Icon
{
    public class PackIconService : PackObjectServiceBase, IPackObjectService
    {
        public PackIconService(IFileService fileService) : base(fileService)
        {
        }
    }
}