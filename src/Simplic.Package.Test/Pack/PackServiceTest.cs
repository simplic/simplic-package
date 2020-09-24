using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Simplic.Package.Service;
using Unity;
using Moq;
using System.IO.Compression;
using System.IO;

namespace Simplic.Package.Test
{
    public class PackServiceTest
    {
        [Fact]
        public async void WriteToEntry_WritingToEntry_Test()
        {
            var container = new UnityContainer();
            container.RegisterType<IPackService, PackService>();

            var entryName = "TestEntry";
            var contentString = "{TestContent}";
            var contentBytes = Encoding.Default.GetBytes(contentString);

            using (var stream = new MemoryStream())
            {
                using (var zipArchive = new ZipArchive(stream, ZipArchiveMode.Create, true))
                {
                    var entry = zipArchive.CreateEntry(entryName);
                    var service = container.Resolve<PackService>();

                    await service.WriteToEntry(entry, contentBytes);
                }

                // Test if it worked
                stream.Position = 0;
                using (var zipArchive = new ZipArchive(stream, ZipArchiveMode.Read))
                {
                    var entry = zipArchive.GetEntry(entryName);

                    using (var memoryStream = new MemoryStream())
                    {
                        using (var entryStream = entry.Open())
                        {
                            await entryStream.CopyToAsync(memoryStream);
                            var entryContentBytes = memoryStream.ToArray();

                            Assert.Equal(contentBytes, entryContentBytes);
                            Assert.Equal(contentString, Encoding.Default.GetString(entryContentBytes));
                        }
                    }
                }
            }
        }

        [Fact]
        public async void Pack_WritingConfigFile_Test()
        {
            var packageConfiguration = new PackageConfiguration
            {
                Name = "",
                Version = new Version(),
                Dependencies = new List<Dependency>(),
                Objects = new Dictionary<string, IList<ObjectListItem>>()
            };

            var container = new UnityContainer();
            container.RegisterType<IPackService, PackService>();

            var service = container.Resolve<IPackService>();

            var streamBytes = await service.Pack(packageConfiguration);

            using (var stream = new MemoryStream())
            {
                foreach (var _byte in streamBytes)
                    stream.WriteByte(_byte);

                using (var zipArchive = new ZipArchive(stream, ZipArchiveMode.Read))
                {
                    var package = zipArchive.GetEntry("package.json");

                    Assert.NotNull(package);
                }
            }
        }

        [Fact]
        public async void Pack_WritingObjectFiles_Test()
        {
            var packageConfiguration = new PackageConfiguration()
            {
                Objects = new Dictionary<string, IList<ObjectListItem>>
                {
                    ["sql"] = new List<ObjectListItem>
                    {
                        new ObjectListItem {
                            Source = "source/Source",
                            Target = "target/Target",
                            Deserialize = true
                        },
                        new ObjectListItem {
                            Source = "source/Source",
                            Target = "target/Target",
                            Deserialize = false
                        }
                    }
                }
            };

            for (int i = 0; i < 12; i++)
                packageConfiguration.Objects["sql"].Add(new ObjectListItem());

            var container = new UnityContainer();
            container.RegisterType<IPackService, PackService>();
            container.RegisterType<IPackObjectService, PackSqlService>("sql");

            var fileService = new Mock<IFileService>();
            fileService.Setup(x => x.ReadAllBytesAsync(It.IsAny<string>())).Returns(Task.FromResult(new byte[] { 0, 1 }));

            container.RegisterInstance<IFileService>(fileService.Object);


            var service = container.Resolve<IPackService>();

            var streamBytes = await service.Pack(packageConfiguration);

            using (var stream = new MemoryStream())
            {
                foreach (var _byte in streamBytes)
                    stream.WriteByte(_byte);

                using (var zipArchive = new ZipArchive(stream, ZipArchiveMode.Read))
                {
                    // Check if right amount of Objects written to Archive. Remeber that the configuration file package.json is also written to the archive
                    Assert.Equal(zipArchive.Entries.Count(), packageConfiguration.Objects.Values.First().Count() + 1);
                }
            }
        }
    }
}
