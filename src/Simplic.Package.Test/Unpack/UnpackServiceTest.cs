using Simplic.Package.Service;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Xunit;

namespace Simplic.Package.Test
{
    public class UnpackServiceTest
    {
        public static IEnumerable<object[]> ConfigurationJsonTestData
        {
            get
            {
                yield return new object[]
                {
                    @"{'PackageFormatVersion': '1.1.1.1',
                      'name': 'Sample-PlugIn',
                      'version': '1.0.0.0',
                      'dependencies': [
                        {
                          'package': 'Simplic ERP',
                          'version': '1.2.320.912',
                          'GreaterAllowed': true
                        }
                      ],
                      'objects': {
                        'sql': [
                          {
                            'source': 'sql/it_document.sql',
                            'target': 'it_document.sql'
                            'mode': 1
                          }
                        ],
                        'repository': [
                          {
                            'source': '/src/Sample/bin/Debug/Sample.dll',
                            'target': '/bin/Sample.dll',
                            'mode': 1
                          }
                        ],
                        'grid': [
                          {
                            'source': 'grid/contact.json',
                            'target': 'contact.json'
                            'mode': 0
                          }
                        ]
                      }
                    }",
                    false
                };
                yield return new object[]
                {
                    @"'{PackageFormatVersion': '1.1.1.1',
                      'name': 'Sample-PlugIn',
                      'version': '1.0.0.0',
                      'dependencies': [
                        {
                          'package': 'Simplic ERP',
                          'version': '1.2.320.912',
                          'GreaterAllowed': true
                        }
                      ],
                      'objects': {}
                    }",
                    false
                };
                //yield return new object[]
                //{
                //    @"{PackageFormatVersion': '1.1.1.1',
                //      'name': 'Sample-PlugIn',
                //      'version': '1.0.0.0',
                //      'dependencies': []}",
                //    true
                //};
            }
        }

        [Fact]
        public async Task Unpack_MissingPackageConfigurationFile_Test()
        {
            var container = DependencyInjectionHelper.GetContainer();

            var unpackService = container.Resolve<IUnpackService>();
            using (var stream = new MemoryStream())
            {
                using (var zipArchive = new ZipArchive(stream, ZipArchiveMode.Create, true))
                {
                }
                stream.Position = 0;

                await Assert.ThrowsAsync<InvalidPackageException>(() => unpackService.Unpack(stream.ToArray()));
            }
        }

        [Theory]
        [MemberData(nameof(ConfigurationJsonTestData))]
        public async Task Unpack_CantDeserializeConfiguration_Test(string json, bool exceptionExpected)
        {
            var container = DependencyInjectionHelper.GetContainer();
            var service = container.Resolve<IUnpackService>();

            using (var stream = new MemoryStream())
            {
                using (var zipArchive = new ZipArchive(stream, ZipArchiveMode.Create, true))
                {
                    var entry = zipArchive.CreateEntry("package.json");

                    using (var memoryStream = new MemoryStream(Encoding.Default.GetBytes(json)))
                    {
                        using (var entryStream = entry.Open())
                        {
                            await memoryStream.CopyToAsync(entryStream);
                        }
                    }
                    stream.Position = 0;

                    if (exceptionExpected)
                    {
                        await Assert.ThrowsAsync<PackageConfigurationException>(() => service.Unpack(stream.ToArray()));
                    }
                    else
                    {
                        var exception = await Record.ExceptionAsync(async () => await service.Unpack(stream.ToArray()));
                        Assert.IsNotType<PackageConfigurationException>(exception);
                    }

                }
            }
        }
    }
}