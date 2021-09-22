namespace Simplic.Package.StackContextArea
{
    /// <summary>
    /// Service to pack stack context area services.
    /// </summary>
    public class PackStackContextAreaService : PackObjectServiceBase, IPackObjectService
    {
        /// <summary>
        /// Initializes a new instace of <see cref="PackStackContextAreaService"/>.
        /// </summary>
        /// <param name="fileService"></param>
        public PackStackContextAreaService(IFileService fileService) : base(fileService)
        {
        }
    }
}