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
        Task UninstallObject(InstallableObject installableObject);

        /// <summary>
        /// NOT IMPLEMENTED!
        /// Force installs an Object by first uninstalling an existing one and then installing the new one
        /// </summary>
        /// <param name="installableObject">The Object to overwrite</param>
        /// <returns></returns>
        Task OverwriteObject(InstallableObject installableObject);

        /// <summary>
        /// Checks if a given Object can be migrated
        /// </summary>
        /// <param name="installableObject">The Object to check</param>
        /// <returns>A CheckMigrationResult object</returns>
        Task<CheckMigrationResult> CheckMigration(InstallableObject installableObject);
    }
}