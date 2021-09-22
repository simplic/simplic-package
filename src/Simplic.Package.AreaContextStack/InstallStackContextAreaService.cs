using System;
using System.Threading.Tasks;
using Unity;

namespace Simplic.Package.StackContextArea
{

    /// <summary>
    /// Service to install stack context areas.
    /// </summary>
    public class InstallStackContextAreaService : IInstallObjectService
    {
        private readonly IObjectRepository repository;

        /// <summary>
        /// Initializes a new instace of <see cref="InstallStackContextAreaService"/>
        /// </summary>
        /// <param name="repository"></param>
        public InstallStackContextAreaService([Dependency("stackContextArea")] IObjectRepository repository)
        {
            this.repository = repository;
        }

        /// <inheritdoc/>
        public async Task<InstallObjectResult> InstallObject(InstallableObject installableObject)
        {
            return await repository.InstallObject(installableObject);
        }

        /// <inheritdoc/>
        public Task<UninstallObjectResult> UninstallObject(InstallableObject installableObject)
        {
            throw new NotImplementedException();
        }
    }
}