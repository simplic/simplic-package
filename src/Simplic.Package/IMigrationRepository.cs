using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package
{
    /// <summary>
    /// Repository for checking and adding migration related data
    /// </summary>
    public interface IMigrationRepository
    {
        /// <summary>
        /// Checks whether a given object can be migrated
        /// </summary>
        /// <param name="installableObject">The object to check</param>
        /// <returns>A CheckMigrationResult object</returns>
        Task<CheckMigrationResult> CheckMigration(InstallableObject installableObject);

        /// <summary>
        /// Adds a migration entryfor a given objet to the database
        /// </summary>
        /// <param name="installableObject">The object whose entry to add</param>
        Task AddMigration(InstallableObject installableObject);
    }
}
