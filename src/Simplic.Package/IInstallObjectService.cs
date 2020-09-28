using System.Threading.Tasks;

namespace Simplic.Package
{
    public interface IInstallObjectService
    {
        /// <summary>
        /// Installs an existing object using the given mode
        /// </summary>
        /// <param name="installableObject">The object to install</param>
        /// <param name="mode">The mode in which the object should be installed</param>
        /// <returns></returns>
        Task Install(InstallableObject installableObject, MigrationMode mode);

        /// <summary>
        /// Installs an existing Object
        /// </summary>
        /// <param name="installableObject">The Object to install</param>
        /// <returns></returns>
        ///
        /// Exceptions:
        /// Object exists, Invalid Object Exception (Object has no target / content), Database Connection failed, various DatabaseExceptions
        Task InstallObject(InstallableObject installableObject);

        /// <summary>
        /// Uninstalls an existing Object
        /// </summary>
        /// <param name="installableObject">The Object to uninstall</param>
        /// <returns></returns>
        Task UninstallObject(InstallableObject installableObject);

        /// <summary>
        /// Force installs an Object by first uninstalling an existing one and then installing the new one
        /// </summary>
        /// <param name="installableObject">The Object to overwrite</param>
        /// <returns></returns>
        Task OverwriteObject(InstallableObject installableObject);
    }
}