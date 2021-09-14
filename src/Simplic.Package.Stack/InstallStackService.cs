using System;
using System.Threading.Tasks;
using Unity;

namespace Simplic.Package.Stack
{
    /// <summary>
    /// Service to install a stack.
    /// </summary>
    public class InstallStackService : IInstallObjectService
    {
        private readonly IObjectRepository repository;

        /// <summary>
        /// Initializes a new instance of <see cref="InstallStackService"/>.
        /// </summary>
        /// <param name="repository"></param>
        public InstallStackService([Dependency("stack")] IObjectRepository repository)
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