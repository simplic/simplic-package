namespace Simplic.Package.Stack
{
    public class PackStackService : PackObjectServiceBase, IPackObjectService
    {
        public PackStackService(IFileService fileService) : base(fileService)
        {
        }
    }
}