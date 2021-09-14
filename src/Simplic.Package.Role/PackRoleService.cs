namespace Simplic.Package.Role
{
    /// <summary>
    /// Service to pack a role.
    /// </summary>
    public class PackRoleService : PackObjectServiceBase, IPackObjectService
    {
        /// <summary>
        /// Initializes a new instance of <see cref="PackRoleService"/>.
        /// </summary>
        /// <param name="fileService"></param>
        public PackRoleService(IFileService fileService) : base(fileService)
        {
        }
    }
}