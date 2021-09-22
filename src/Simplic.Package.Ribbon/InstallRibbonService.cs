using System;
using System.Threading.Tasks;

namespace Simplic.Package.Ribbon
{
    /// <summary>
    /// Service to install ribbon items.
    /// </summary>
    public class InstallRibbonService : IInstallObjectService
    {
        private readonly IObjectRepository repository;

        /// <summary>
        /// Initializes a new instance of <see cref="InstallRibbonService"/>.
        /// </summary>
        /// <param name="repository"></param>
        public InstallRibbonService([Unity.Dependency("ribbon")] IObjectRepository repository)
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