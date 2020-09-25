using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package.Service
{
    public class InstallService : IInstallService
    {
        public async Task Install(UnpackedPackage unpackedPackage)
        {
        }

        public async Task Uninstall(UnpackedPackage unpackedPackage)
        {
        }

        public async Task Overwrite(UnpackedPackage unpackedPackage)
        {
            Uninstall(unpackedPackage);
            Install(unpackedPackage);
        }
    }
}
