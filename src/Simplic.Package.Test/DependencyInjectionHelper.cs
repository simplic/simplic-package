using Simplic.Package.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Lifetime;
using Xunit.Sdk;

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

            container.RegisterType<IPackObjectService, PackSqlService>("sql");
            container.RegisterType<IPackObjectService, PackRepositoryService>("repository");
            container.RegisterType<IPackObjectService, PackGridService>("grid");
            return container;
        }
    }
}
