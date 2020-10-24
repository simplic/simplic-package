using System;
using System.Threading.Tasks;
using Unity;

namespace Simplic.Package.Stack
{
    public class InstallStackService : IInstallObjectService
    {
        private readonly IObjectRepository repository;

        public InstallStackService([Dependency("stack")] IObjectRepository repository)
        {
            this.repository = repository;
        }

        public async Task<InstallObjectResult> InstallObject(InstallableObject installableObject)
        {
            return await repository.InstallObject(installableObject);
        }

        public Task<UninstallObjectResult> UninstallObject(InstallableObject installableObject)
        {
            throw new NotImplementedException();
        }
    }
}