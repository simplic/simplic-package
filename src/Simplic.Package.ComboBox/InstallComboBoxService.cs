using System;
using System.Threading.Tasks;
using Unity;

namespace Simplic.Package.ComboBox
{
    public class InstallComboBoxService : IInstallObjectService
    {
        private readonly IObjectRepository repository;

        public InstallComboBoxService([Dependency("comboBox")] IObjectRepository repository)
        {
            this.repository = repository;
        }

        public async Task<InstallObjectResult> InstallObject(InstallableObject installableObject)
        {
            return await repository.InstallObject(installableObject);
        }

        public async Task<UninstallObjectResult> UninstallObject(InstallableObject installableObject)
        {
            return await repository.UninstallObject(installableObject);
        }
    }
}