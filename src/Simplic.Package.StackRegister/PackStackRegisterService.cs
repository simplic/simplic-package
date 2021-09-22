namespace Simplic.Package.StackRegister
{
    /// <summary>
    /// Service to pack a stack register.
    /// </summary>
    public class PackStackRegisterService : PackObjectServiceBase, IPackObjectService
    {
        /// <summary>
        /// Initializes a new instance of <see cref="PackStackRegisterService"/>.
        /// </summary>
        /// <param name="fileService"></param>
        public PackStackRegisterService(IFileService fileService) : base(fileService)
        {
        }
    }
}