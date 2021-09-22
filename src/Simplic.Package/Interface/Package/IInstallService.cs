using System.Threading.Tasks;

namespace Simplic.Package
{
    /// <summary>
    /// Service for installing packages.
    /// </summary>
    public interface IInstallService
    {
        /// <summary>
        /// Installs a package.
        /// </summary>
        /// <param name="unpackedPackage">The package to install.</param>
        /// <returns></returns>
        Task Install(Package unpackedPackage);

        /// <summary>
        /// NOT IMPLEMENTED!!
        /// Uninstalls an existing package.
        /// </summary>
        /// <param name="unpackedPackage">The package to uninstall.</param>
        /// <returns></returns>
        Task Uninstall(Package unpackedPackage);
    }
}