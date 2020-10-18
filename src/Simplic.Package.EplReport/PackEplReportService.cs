namespace Simplic.Package.EplReport
{
    public class PackEplReportService : PackObjectServiceBase, IPackObjectService
    {
        public PackEplReportService(IFileService fileService) : base(fileService)
        {
        }
    }
}