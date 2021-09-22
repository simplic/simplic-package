using System;
using System.Threading.Tasks;
using Unity;

namespace Simplic.Package.EplReport
{
    /// <summary>
    /// Service to install epl reports.
    /// </summary>
    public class InstallEplReportService : IInstallObjectService
    {
        private readonly IObjectRepository repository;

        /// <summary>
        /// Initializes a new instance of <see cref="InstallEplReportService"/>.
        /// </summary>
        /// <param name="repository"></param>
        public InstallEplReportService([Dependency("eplReport")] IObjectRepository repository)
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