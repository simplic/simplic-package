using System.Threading.Tasks;

namespace Simplic.Package.Service
{
    /// <inheritdoc cref="IMigrationService"/>
    public class MigrationService : IMigrationService
    {
        private readonly IMigrationRepository repository;

        /// <summary>
        /// Initializes a new instance of <see cref="MigrationService"/>
        /// </summary>
        /// <param name="repository"></param>
        public MigrationService(IMigrationRepository repository)
        {
            this.repository = repository;
        }

        /// <inheritdoc/>
        public async Task<CheckMigrationResult> CheckMigration(InstallableObject installableObject) => await repository.CheckMigration(installableObject);

        // TODO: This could return a result object aswell, if information should be logged
        /// <inheritdoc/>
        public async Task AddMigration(InstallableObject installableObject) => await repository.AddMigration(installableObject);
    }
}