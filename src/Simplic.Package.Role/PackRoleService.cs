namespace Simplic.Package.Role
{
    public class PackRoleService : PackObjectServiceBase, IPackObjectService
    {
        public PackRoleService(IFileService fileService) : base(fileService)
        {
        }
    }
}