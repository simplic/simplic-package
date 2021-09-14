namespace Simplic.Package.Stack
{
    /// <summary>
    /// Service to pack stacks.
    /// </summary>
    public class PackStackService : PackObjectServiceBase, IPackObjectService
    {
        /// <summary>
        /// Initializes a new instance of <see cref="PackStackService"/>.
        /// </summary>
        /// <param name="fileService"></param>
        public PackStackService(IFileService fileService) : base(fileService)
        {
        }
    }
}