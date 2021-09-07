using System;
using System.Threading.Tasks;
using Unity;

namespace Simplic.Package.FormatList
{
    /// <summary>
    /// Service to install format lists.
    /// </summary>
    public class InstallFormatListService : IInstallObjectService
    {
        private readonly IObjectRepository repository;

        /// <summary>
        /// Initializes a new instance of <see cref="InstallFormatListService"/>.
        /// </summary>
        /// <param name="repository"></param>
        public InstallFormatListService([Dependency("formatList")] IObjectRepository repository)
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