using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package.Service
{
    public class FileService : IFileService
    {
        public async Task<byte[]> ReadAllBytesAsync(string path)
        {
            return File.ReadAllBytes(path);
        }
    }
}
