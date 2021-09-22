using System;
using System.Threading.Tasks;
using Unity;

namespace Simplic.Package.StackAutoconnector
{
    /// <summary>
    /// Service to install stack autoconnectors.
    /// </summary>
    public class InstallStackAutoconnectorService : IInstallObjectService
    {
        private readonly IObjectRepository repository;

        /// <summary>
        /// Initializes a new instance of <see cref="InstallStackAutoconnectorService"/>.
        /// </summary>
        /// <param name="repository"></param>
        public InstallStackAutoconnectorService([Dependency("stackAutoconnector")] IObjectRepository repository)
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