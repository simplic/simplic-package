using System.ComponentModel;
using System.IO.Pipes;
using System.Threading.Tasks;
using Unity;

namespace Simplic.Package.Service
{
    public class InstallService : IInstallService
    {
        private readonly IUnityContainer container;
        public InstallService(IUnityContainer container)
        {
            this.container = container;
        }

        public async Task Install(Package unpackedPackage)
        {
            foreach (var dependency in unpackedPackage.Dependencies)
            {
                // Check if depency exists and give information if it doesnt
            }

            foreach (var item in unpackedPackage.UnpackedObjects)
            {
                var installService = container.Resolve <IInstallObjectService>(item.Key);

                foreach (var installableObject in item.Value)
                {
                    await installService.InstallObject(installableObject);
                }
            }
        }

        public async Task Uninstall(Package unpackedPackage)
        {
            foreach (var item in unpackedPackage.UnpackedObjects)
            {
                var installService = container.Resolve<IInstallObjectService>(item.Key);

                foreach (var installableObject in item.Value)
                {
                    await installService.UninstallObject(installableObject);
                }
            }
        }

        public async Task Overwrite(Package unpackedPackage)
        {
            await Uninstall(unpackedPackage);
            await Install(unpackedPackage);
        }
    }
}