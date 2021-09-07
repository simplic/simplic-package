namespace Simplic.Package.FormatList
{
    /// <summary>
    /// Service to pack format lists.
    /// </summary>
    public class PackFormatListService : PackObjectServiceBase, IPackObjectService
    {
        /// <summary>
        /// Initializes a new instance of <see cref="PackFormatListService"/>.
        /// </summary>
        /// <param name="fileService"></param>
        public PackFormatListService(IFileService fileService) : base(fileService)
        {
        }
    }
}