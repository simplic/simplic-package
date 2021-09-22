using System.Threading.Tasks;

namespace Simplic.Package
{
    /// <summary>
    /// Service for checking and adding migration related data.
    /// </summary>
    public interface IMigrationService
    {
        /// <summary>
        /// Checks whether a given object can be migrated.
        /// </summary>
        /// <param name="installableObject">The object to check.</param>
        /// <returns>A <see cref="CheckMigrationResult"/> object.</returns>
        Task<CheckMigrationResult> CheckMigration(InstallableObject installableObject);

        /// <summary>
        /// Adds a migration entry for a given object to the database.
        /// </summary>
        /// <param name="installableObject">The object whose entry to add.</param>
        Task AddMigration(InstallableObject installableObject);
    }
}