namespace Simplic.Package.Grid
{
    public class PackGridService : PackObjectServiceBase, IPackObjectService
    {
        public PackGridService(IFileService fileService) : base(fileService)
        {
        }
    }
}