using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace Simplic.Package.StackContextArea
{
    public class InstallStackContextAreaService : IInstallObjectService
    {
        private readonly IObjectRepository repository;

        public InstallStackContextAreaService([Dependency("stackContextArea")] IObjectRepository repository)
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

        public Task UninstallObject(InstallableObject installableObject)
        {
            throw new NotImplementedException();
        }
    }
}
