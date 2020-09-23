using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
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

        public async Task<string> ReadAllTextAsync(string path)
        {
            var byteArray = await ReadAllBytesAsync(path);

            return Encoding.Default.GetString(byteArray);
        }

        public async Task<byte[]> ReadAllBytesAsync(Stream stream)
        {
            using (stream)
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    await stream.CopyToAsync(memoryStream);

                    return memoryStream.ToArray();
                }
            }
        }
        // So muss nur einmal asynchron gelesen werden
        public async Task<string> ReadAllTextAsync(Stream stream)
        {
            var byteArray = await ReadAllBytesAsync(stream);

            return Encoding.Default.GetString(byteArray);
        }
    }
}
