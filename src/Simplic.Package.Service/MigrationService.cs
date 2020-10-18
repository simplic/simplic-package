using Dapper;
using Simplic.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package.Service
{
    public class MigrationService : IMigrationService
    {
        private readonly IMigrationRepository repository;
        public MigrationService(IMigrationRepository repository)
        {
            this.repository = repository;
        }

        public async Task<CheckMigrationResult> CheckMigration(InstallableObject installableObject)
        {
            return await repository.CheckMigration(installableObject);
        }

        // TODO: This could return a result object aswell, if information should be logged
        public async Task AddMigration(InstallableObject installableObject)
        {
            await repository.AddMigration(installableObject);
        }
    }
}