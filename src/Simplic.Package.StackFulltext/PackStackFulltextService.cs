namespace Simplic.Package.StackFulltext
{
    /// <summary>
    /// Service to pack a stack fulltext.
    /// </summary>
    public class PackStackFulltextService : PackObjectServiceBase, IPackObjectService
    {
        /// <summary>
        /// Initializes a new instace of <see cref="PackStackFulltextService"/>.
        /// </summary>
        /// <param name="fileService"></param>
        public PackStackFulltextService(IFileService fileService) : base(fileService)
        {
        }
    }
}