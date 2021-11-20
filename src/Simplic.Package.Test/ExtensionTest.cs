using Moq;
using Simplic.Package.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Xunit;

namespace Simplic.Package.Test
{
    public class ExtensionTest
    {
        [Fact]
        public void LoadExtension_LaodsExtension()
        {
            var container = new UnityContainer();
            var logService = DependencyInjectionHelper.GetContainer().Resolve<ILogService>();

            var solutionDir = Directory
                .GetParent(Assembly.GetExecutingAssembly().Location)
                .Parent.Parent.Parent.FullName;

            var sampleDir = Path.Combine(solutionDir, "..\\sample\\");

            Directory.SetCurrentDirectory(sampleDir);

            var service = new ExtensionService(logService, container);



        }


    }
}
