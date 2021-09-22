using System;
using System.Threading.Tasks;
using Unity;

namespace Simplic.Package.Sql
{
    /// <summary>
    /// Service to install an sql export.
    /// </summary>
    public class InstallSqlService : IInstallObjectService
    {
        private readonly IObjectRepository repository;

        /// <summary>
        /// Initializes a new instance of <see cref="InstallSqlService"/>.
        /// </summary>
        /// <param name="repository"></param>
        public InstallSqlService([Dependency("sql")] IObjectRepository repository)
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