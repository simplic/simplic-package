using System;
using System.Threading.Tasks;
using Unity;

namespace Simplic.Package.ItemBox
{
    /// <summary>
    /// Service to install an itembox. 
    /// </summary>
    public class InstallItemBoxService : IInstallObjectService
    {
        private readonly IObjectRepository repository;

        /// <summary>
        /// Initializes a new instance of <see cref="InstallItemBoxService"/>.
        /// </summary>
        /// <param name="repository"></param>
        public InstallItemBoxService([Dependency("itemBox")] IObjectRepository repository)
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