namespace Simplic.Package.Icon
{
    /// <summary>
    /// Service to pack an icon.
    /// </summary>
    public class PackIconService : PackObjectServiceBase, IPackObjectService
    {

        /// <summary>
        /// Initializes a new instance of <see cref="PackIconService"/>.
        /// </summary>
        /// <param name="fileService"></param>
        public PackIconService(IFileService fileService) : base(fileService)
        {
        }
    }
}