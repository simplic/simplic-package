using System;
using System.Threading.Tasks;
using Unity;

namespace Simplic.Package.EplReportDesign
{
    public class InstallEplReportDesignService : IInstallObjectService
    {
        private readonly IObjectRepository repository;

        public InstallEplReportDesignService([Dependency("eplReportDesign")] IObjectRepository repository)
        {
            this.repository = repository;
        }

        public Task<CheckMigrationResult> CheckMigration(InstallableObject installableObject)
        {
            throw new NotImplementedException();
        }

        public async Task<InstallObjectResult> InstallObject(InstallableObject installableObject)
        {
            return await repository.InstallObject(installableObject);
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