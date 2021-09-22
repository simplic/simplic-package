using System;
using System.Threading.Tasks;
using Unity;

namespace Simplic.Package.StackFulltext
{
    /// <summary>
    /// Service to install a stack fulltext.
    /// </summary>
    public class InstallStackFulltextService : IInstallObjectService
    {
        private readonly IObjectRepository repository;

        /// <summary>
        /// Initiaializes a new instance of <see cref="InstallStackFulltextService"/>.
        /// </summary>
        /// <param name="repository"></param>
        public InstallStackFulltextService([Dependency("stackFulltext")] IObjectRepository repository)
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