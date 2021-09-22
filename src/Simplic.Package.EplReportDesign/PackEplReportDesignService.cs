namespace Simplic.Package.EplReportDesign
{
    /// <summary>
    /// Service to pack rpl report designs.
    /// </summary>
    public class PackEplReportDesignService : PackObjectServiceBase, IPackObjectService
    {
        /// <summary>
        /// Initializes a new instance of <see cref="PackEplReportDesignService"/>.
        /// </summary>
        /// <param name="fileService"></param>
        public PackEplReportDesignService(IFileService fileService) : base(fileService)
        {
        }
    }
}