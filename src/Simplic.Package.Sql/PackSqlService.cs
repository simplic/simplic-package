using System.Threading.Tasks;

namespace Simplic.Package.Sql
{
    public class PackSqlService : PackObjectServiceBase, IPackObjectService
    {
        public PackSqlService(IFileService fileService) : base(fileService)
        {
        }
    }
}