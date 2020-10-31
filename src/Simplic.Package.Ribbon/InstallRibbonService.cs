using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Unity;

namespace Simplic.Package.Ribbon
{
    public class InstallRibbonService : IInstallObjectService
    {
        private readonly IObjectRepository repository;

        public InstallRibbonService([Unity.Dependency("ribbon")] IObjectRepository repository)
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