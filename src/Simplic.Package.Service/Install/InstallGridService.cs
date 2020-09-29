using Simplic.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace Simplic.Package.Service.Install
{
    public class InstallGridService : IInstallObjectService
    {
        private readonly IObjectRepository repository; // Unity container
        public InstallGridService([Dependency("grid")] IObjectRepository repository)
        {
            this.repository = repository;
        }

        public Task<CheckMigrationResult> CheckMigration(InstallableObject installableObject)
        {
            throw new NotImplementedException();
        }

        public async Task InstallObject(InstallableObject installableObject)
        {
            // Todo: Automapper und Simplic API
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
