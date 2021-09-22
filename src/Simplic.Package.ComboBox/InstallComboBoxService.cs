using System;
using System.Threading.Tasks;
using Unity;

namespace Simplic.Package.ComboBox
{
    /// <summary>
    /// Service to install combo boxes.
    /// </summary>
    public class InstallComboBoxService : IInstallObjectService
    {
        private readonly IObjectRepository repository;

        /// <summary>
        /// Initializes a new isntance of <see cref="InstallComboBoxService"/>.
        /// </summary>
        /// <param name="repository"></param>
        public InstallComboBoxService([Dependency("comboBox")] IObjectRepository repository)
        {
            this.repository = repository;
        }

        /// <inheritdoc/>
        public async Task<InstallObjectResult> InstallObject(InstallableObject installableObject)
        {
            return await repository.InstallObject(installableObject);
        }

        /// <inheritdoc/>
        public async Task<UninstallObjectResult> UninstallObject(InstallableObject installableObject)
        {
            return await repository.UninstallObject(installableObject);
        }
    }
}