using Simplic.Package.Service;
using Unity;
using Unity.Lifetime;

namespace Simplic.Package.Test
{
    public static class DependencyInjectionHelper
    {
        public static IUnityContainer GetContainer()
        {
            var container = new UnityContainer();

            container.RegisterType<IUnpackService, UnpackService>();
            container.RegisterType<IFileService, FileService>();
            container.RegisterType<ILogService, LogService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IPackService, PackService>();
            container.RegisterType<IInstallService, InstallService>();

            return container;
        }
    }
}