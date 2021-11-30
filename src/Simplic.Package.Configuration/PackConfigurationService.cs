namespace Simplic.Package.Configuration
{
    /// <summary>
    /// Service to pack configurations.
    /// </summary>
    public class PackConfigurationService : PackObjectServiceBase, IPackObjectService
    {
        /// <summary>
        /// Initializes a new instace of <see cref="PackConfigurationService"/>.
        /// </summary>
        /// <param name="fileService"></param>
        public PackConfigurationService(IFileService fileService) : base(fileService)
        {
        }
    }
}
