namespace Simplic.Package.Grid
{
    /// <summary>
    /// Service to pack a grid.
    /// </summary>
    public class PackGridService : PackObjectServiceBase, IPackObjectService
    {
        /// <summary>
        /// Initializes a new instance of <see cref="PackGridService"/>.
        /// </summary>
        /// <param name="fileService"></param>
        public PackGridService(IFileService fileService) : base(fileService)
        {
        }
    }
}