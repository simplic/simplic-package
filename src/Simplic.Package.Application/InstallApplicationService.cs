using System;
using System.Threading.Tasks;
using Unity;

namespace Simplic.Package.Application
{
    /// <summary>
    /// Service to install applications.
    /// </summary>
    public class InstallApplicationService : IInstallObjectService
    {
        private readonly IObjectRepository repository;

        /// <summary>
        /// Initializes a new instance of <see cref="InstallApplicationService"/>
        /// </summary>
        /// <param name="repository"></param>
        public InstallApplicationService([Dependency("application")] IObjectRepository repository)
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