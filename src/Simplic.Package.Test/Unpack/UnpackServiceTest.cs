using Simplic.Package.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Xunit;

namespace Simplic.Package.Test.Unpack
{
    public class UnpackServiceTest
    {
        [Fact]
        public async Task Unpack_MissingPackageConfigurationFile_Test()
        {
            var container = new UnityContainer();
            container.RegisterType<IUnpackService, UnpackService>();
            container.RegisterType<IFileService, FileService>();

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
    }
}
