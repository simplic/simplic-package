using System.Threading.Tasks;

namespace Simplic.Package.Sequence
{
    public class PackSequenceService : PackObjectServiceBase, IPackObjectService
    {
        public PackSequenceService(IFileService fileService) : base(fileService)
        {
        }
    }
}