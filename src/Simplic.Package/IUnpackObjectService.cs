using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package
{
    public interface IUnpackObjectService
    {
        Task<UnpackObjectResult> UnpackObject(ZipArchiveEntry entry);
    }
}
