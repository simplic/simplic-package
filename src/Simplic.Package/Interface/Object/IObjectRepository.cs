using System.Threading.Tasks;

namespace Simplic.Package
{
    /// <summary>
    /// Service for writing and checking objects in the database.
    /// </summary>
    public interface IObjectRepository
    {
        /// <summary>
        /// Inserts a given object into the database.
        /// </summary>
        /// <param name="installableObject">The object to insert.</param>
        /// <returns>A InstallObjectResult object.</returns>
        Task<InstallObjectResult> InstallObject(InstallableObject installableObject);

        /// <summary>
        /// Remoes a given object from the database.
        /// </summary>
        /// <param name="installableObject">The object to remove.</param>
        /// <returns>A UninstallObjectResult object.</returns>
        Task<UninstallObjectResult> UninstallObject(InstallableObject installableObject);
    }
}