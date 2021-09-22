using System;
using System.Threading.Tasks;
using Unity;

namespace Simplic.Package.EplReportDesign
{
    /// <summary>
    /// Service to install epl report desings.
    /// </summary>
    public class InstallEplReportDesignService : IInstallObjectService
    {
        private readonly IObjectRepository repository;

        /// <summary>
        /// Initializes a new instance of <see cref="InstallEplReportDesignService"/>.
        /// </summary>
        /// <param name="repository"></param>
        public InstallEplReportDesignService([Dependency("eplReportDesign")] IObjectRepository repository)
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