namespace Simplic.Package.Ribbon
{
    public class PackRibbonService : PackObjectServiceBase, IPackObjectService
    {
        public PackRibbonService(IFileService fileService) : base(fileService)
        {
        }
    }
}