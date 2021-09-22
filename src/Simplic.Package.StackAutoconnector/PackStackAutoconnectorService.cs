namespace Simplic.Package.StackAutoconnector
{
    /// <summary>
    /// Service to pack stack autoconnectors.
    /// </summary>
    public class PackStackAutoconnectorService : PackObjectServiceBase, IPackObjectService
    {

        /// <summary>
        /// Initializes a new instance of <see cref="PackStackAutoconnectorService"/>.
        /// </summary>
        /// <param name="fileService"></param>
        public PackStackAutoconnectorService(IFileService fileService) : base(fileService)
        {
        }
    }
}