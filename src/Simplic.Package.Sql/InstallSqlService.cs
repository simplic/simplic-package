using System;
using System.Threading.Tasks;
using Unity;

namespace Simplic.Package.Sql
{
    public class InstallSqlService : IInstallObjectService
    {
        private readonly IObjectRepository repository;

        public InstallSqlService([Dependency("sql")] IObjectRepository repository)
        {
            this.repository = repository;
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