using Moq;
using Simplic.Package.Service;
using System;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using Unity;
using Xunit;

namespace Simplic.Package.Test
{
    public class SimplePack
    {
        [Fact]
        public async Task TestMethod1()
        {
            var container = new UnityContainer();
            container.RegisterType<IPackService, PackService>();
            container.RegisterType<IPackObjectService, PackSqlService>("sql");

            var fileService = new Mock<IFileService>();
            fileService.Setup(x => x.ReadAllBytesAsync(It.IsAny<string>())).Returns(Task.FromResult(new byte[] { 1, 2, 3 }));

            // Every instance of IFileService will reference fileService ?
            // Does every registered type now take this as reference or how does it work?
            container.RegisterInstance<IFileService>(fileService.Object);

            // Was passiert hier?
            var service = container.Resolve<IPackService>();

            var json = @"
            {
                'name': 'Sample-PlugIn',
                'version': '1.0.0.0',
                'dependencies': [
                  {
                    'package': 'Simplic ERP',
                    'version': '1.2.320.912',
                    'greater_allowed': true
                  }
                ],
                'objects': {
                  'sql': [
                    {
                      'source': 'sql/it_document.sql',
                      'target': 'it_document.sql'
                    }
                  ]
                }
              }
            ";

            await service.Pack(json);
            // Assert.Equal 
            // File.WriteAllBytes(@"C:\temp\test.zip", await service.Pack(json));
        }
    }
}
