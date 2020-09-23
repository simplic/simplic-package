using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package
{
    public interface IUnpackService
    {
        Task<UnpackedPackage> Unpack(string zipArchive);
        Task<UnpackedPackage> Unpack(ZipArchive zipArchive);
    }
}
