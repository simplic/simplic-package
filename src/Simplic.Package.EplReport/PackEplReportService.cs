namespace Simplic.Package.EplReport
{
    /// <summary>
    /// Service to pack epl reports.
    /// </summary>
    public class PackEplReportService : PackObjectServiceBase, IPackObjectService
    {
        /// <summary>
        /// Initializes a new instace of <see cref="PackEplReportService"/>.
        /// </summary>
        /// <param name="fileService"></param>
        public PackEplReportService(IFileService fileService) : base(fileService)
        {
        }
    }
}