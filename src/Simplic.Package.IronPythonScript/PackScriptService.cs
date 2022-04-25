namespace Simplic.Package.IronPythonScript
{
    /// <summary>
    /// Service to pack scripts.
    /// </summary>
    public class PackScriptService : PackObjectServiceBase
    {
        /// <summary>
        /// Initializes a new instace of <see cref="PackScriptService"/>.
        /// </summary>
        /// <param name="fileService"></param>
        public PackScriptService(IFileService fileService) : base(fileService)
        {

        }
    }
}
