using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package.Service
{
    public class FileService : IFileService
    {
        /// <summary>
        /// Reads all bytes from a file at a given path
        /// </summary>
        /// <param name="path">The path to the file</param>
        /// <returns>The read bytes as an array</returns>
        public async Task<byte[]> ReadAllBytesAsync(string path)
        {
            var fileStream = File.OpenRead(path);

            return await ReadAllBytesAsync(fileStream);
        }

        /// <summary>
        /// Reads all text from a file at a given path
        /// </summary>
        /// <param name="path">The path to the file</param>
        /// <returns>The read text as a string</returns>
        public async Task<string> ReadAllTextAsync(string path)
        {
            var byteArray = await ReadAllBytesAsync(path);

            return Encoding.Default.GetString(byteArray);
        }

        /// <summary>
        /// Reads all bytes from a stream
        /// </summary>
        /// <param name="stream">The stream to read from</param>
        /// <returns>The read bytes as an array</returns>
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

        /// <summary>
        /// Reads all text from a stream
        /// </summary>
        /// <param name="stream">The stream to read from</param>
        /// <returns>The read text as a string</returns
        public async Task<string> ReadAllTextAsync(Stream stream)
        {
            var byteArray = await ReadAllBytesAsync(stream);

            return Encoding.Default.GetString(byteArray);
        }

        public Task WriteAllBytesAsync(byte[] bytes, string path)
        {
            throw new System.NotImplementedException();
        }

        public Task WriteAllBytesAsync(Stream stream, string path)
        {
            throw new System.NotImplementedException();
        }

        public Task WriteAllTextAsync(string text, string path)
        {
            throw new System.NotImplementedException();
        }

        public Task WriteAllTextAsync(Stream stream, string path)
        {
            throw new System.NotImplementedException();
        }
    }
}