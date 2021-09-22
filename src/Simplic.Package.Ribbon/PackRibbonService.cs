namespace Simplic.Package.Ribbon
{
    /// <summary>
    /// Service to pack ribbon items.
    /// </summary>
    public class PackRibbonService : PackObjectServiceBase, IPackObjectService
    {
        /// <summary>
        /// Initalizes a new istance of <see cref="PackRibbonService"/>.
        /// </summary>
        /// <param name="fileService"></param>
        public PackRibbonService(IFileService fileService) : base(fileService)
        {
        }
    }
}