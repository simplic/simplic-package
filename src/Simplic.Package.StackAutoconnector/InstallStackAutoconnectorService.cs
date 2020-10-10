using System;
using System.Threading.Tasks;
using Unity;

namespace Simplic.Package.StackAutoconnector
{
    public class InstallStackAutoconnectorService : IInstallObjectService
    {
        private readonly IObjectRepository repository;

        public InstallStackAutoconnectorService([Dependency("stackAutoconnector")] IObjectRepository repository)
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