using System;
using System.Threading.Tasks;
using Unity;

namespace Simplic.Package.Grid
{
    public class InstallGridService : IInstallObjectService
    {
        private readonly IObjectRepository repository;

        public InstallGridService([Dependency("grid")] IObjectRepository repository)
        {
            this.repository = repository;
        }

        public async Task<CheckMigrationResult> CheckMigration(InstallableObject installableObject)
        {
            throw new NotImplementedException();
        }

        public async Task<InstallObjectResult> InstallObject(InstallableObject installableObject)
        {
            // Todo: Automapper and Simplic API
            throw new NotImplementedException();
        }

        public Task OverwriteObject(InstallableObject installableObject)
        {
            throw new NotImplementedException();
        }

        public Task<UninstallObjectResult> UninstallObject(InstallableObject installableObject)
        {
            throw new NotImplementedException();
        }
    }
}