using System.Threading.Tasks;

namespace Simplic.Package
{
    /// <summary>
    /// Service for installing objects
    /// </summary>
    public interface IInstallObjectService
    {
        /// <summary>
        /// Installs an existing Object
        /// </summary>
        /// <param name="installableObject">The Object to install</param>
        /// <returns></returns>
        ///
        /// Exceptions:
        /// Object exists, Invalid Object Exception (Object has no target / content), Database Connection failed, various DatabaseExceptions
        Task<InstallObjectResult> InstallObject(InstallableObject installableObject);

        /// <summary>
        /// NOT IMPLEMENTED!
        /// Uninstalls an existing Object
        /// </summary>
        /// <param name="installableObject">The Object to uninstall</param>
        /// <returns></returns>
        Task<UninstallObjectResult> UninstallObject(InstallableObject installableObject);
    }
}