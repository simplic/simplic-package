namespace Simplic.Package.Application
{
    public class PackApplicationService : PackObjectServiceBase, IPackObjectService
    {
        public PackApplicationService(IFileService fileService) : base(fileService)
        {
        }
    }
}