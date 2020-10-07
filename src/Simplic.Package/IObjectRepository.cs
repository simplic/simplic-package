using System.Threading.Tasks;

namespace Simplic.Package
{
    /// <summary>
    /// Service for writing and checking objects in the database
    /// </summary>
    public interface IObjectRepository
    {
        /// <summary>
        /// Checks if given object is was already migrated to the database
        /// </summary>
        /// <param name="installableObject">The object to check</param>
        /// <returns>A CheckMigrationResult object</returns>
        Task<CheckMigrationResult> CheckMigration(InstallableObject installableObject);

        /// <summary>
        /// Inserts a given object into the database
        /// </summary>
        /// <param name="installableObject">The object to insert</param>
        /// <returns>A InstallObjectResult object</returns>
        Task<InstallObjectResult> InstallObject(InstallableObject installableObject);

        /// <summary>
        /// Remoes a given object from the database
        /// </summary>
        /// <param name="installableObject">The object to remove</param>
        /// <returns>A UninstallObjectResult object</returns>
        Task<UninstallObjectResult> UninstallObject(InstallableObject installableObject);

    }
}