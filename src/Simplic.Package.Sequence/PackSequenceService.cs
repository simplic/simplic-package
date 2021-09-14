namespace Simplic.Package.Sequence
{
    /// <summary>
    /// Service to pack sequences.
    /// </summary>
    public class PackSequenceService : PackObjectServiceBase, IPackObjectService
    {
        /// <summary>
        /// Initializes a new insance of <see cref="PackSequenceService"/>.
        /// </summary>
        /// <param name="fileService"></param>
        public PackSequenceService(IFileService fileService) : base(fileService)
        {
        }
    }
}