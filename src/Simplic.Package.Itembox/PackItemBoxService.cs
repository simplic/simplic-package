namespace Simplic.Package.ItemBox
{
    public class PackItemBoxService : PackObjectServiceBase, IPackObjectService
    {
        public PackItemBoxService(IFileService fileService) : base(fileService)
        {
        }
    }
}