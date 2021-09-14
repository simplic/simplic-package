namespace Simplic.Package.Repository
{
    /// <summary>
    /// Service to pack a repository item.
    /// </summary>
    public class PackRepositoryService : PackObjectServiceBase, IPackObjectService
    {
        /// <summary>
        /// Initializes a new instance of <see cref="PackRepositoryService"/>.
        /// </summary>
        /// <param name="fileService"></param>
        public PackRepositoryService(IFileService fileService) : base(fileService)
        {
        }
    }
}