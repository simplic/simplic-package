namespace Simplic.Package.ItemBox
{
    /// <summary>
    /// Service to pack itemboxes.
    /// </summary>
    public class PackItemBoxService : PackObjectServiceBase, IPackObjectService
    {
        /// <summary>
        /// Initializes a new istance of <see cref="PackItemBoxService"/>.
        /// </summary>
        /// <param name="fileService"></param>
        public PackItemBoxService(IFileService fileService) : base(fileService)
        {
        }
    }
}