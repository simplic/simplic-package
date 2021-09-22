using System;
using System.Threading.Tasks;
using Unity;

namespace Simplic.Package.StackRegister
{
    /// <summary>
    /// Service to install a stack register.
    /// </summary>
    public class InstallStackRegisterService : IInstallObjectService
    {
        private readonly IObjectRepository repository;

        /// <summary>
        /// Initializes a new instance of <see cref="InstallStackRegisterService"/>.
        /// </summary>
        /// <param name="repository"></param>
        public InstallStackRegisterService([Dependency("stackRegister")] IObjectRepository repository)
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