using Simplic.Package.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Unity;
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

            container.RegisterType<IPackObjectService, PackSqlService>("sql");
            container.RegisterType<IPackObjectService, PackRepositoryService>("repository");
            container.RegisterType<IPackObjectService, PackGridService>("grid");

            return container;
        }
    }
}
