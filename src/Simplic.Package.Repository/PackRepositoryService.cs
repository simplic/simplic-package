namespace Simplic.Package.Repository
{
    public class PackRepositoryService : PackObjectServiceBase, IPackObjectService
    {
        public PackRepositoryService(IFileService fileService) : base(fileService)
        {
        }
    }
}