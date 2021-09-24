namespace Simplic.Package.Application
{
    /// <summary>
    /// Service to pack applications.
    /// </summary>
    public class PackApplicationService : PackObjectServiceBase, IPackObjectService
    {
        /// <summary>
        /// Initializes a new instance of <see cref="PackApplicationService"/>.
        /// </summary>
        /// <param name="fileService"></param>
        public PackApplicationService(IFileService fileService) : base(fileService)
        {
        }
    }
}