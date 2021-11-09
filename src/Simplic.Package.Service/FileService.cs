using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package.Service
{
    /// <inheritdoc cref="IFileService"/>
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

            return Encoding.UTF8.GetString(byteArray);
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

            return Encoding.UTF8.GetString(byteArray);
        }

        /// <inheritdoc/>
        public async Task WriteAllBytesAsync(byte[] bytes, string path)
        {
            using (var stream = new FileStream(path, FileMode.OpenOrCreate))
            {
                stream.Seek(0, SeekOrigin.End);
                await stream.WriteAsync(bytes, 0, bytes.Length);
            }
        }

        /// <inheritdoc/>
        public async Task WriteAllBytesAsync(Stream stream, string path)
        {
            using (var fileStream = new FileStream(path, FileMode.OpenOrCreate))
            {
                using (var memoryStream = new MemoryStream())
                {
                    await stream.CopyToAsync(memoryStream);
                    fileStream.Seek(0, SeekOrigin.End);
                    await fileStream.WriteAsync(memoryStream.ToArray(), 0, memoryStream.ToArray().Length);
                }
            }
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public async Task WriteAllTextAsync(string text, string path)
        {
            using (var fileStream = new FileStream(path, FileMode.OpenOrCreate))
            {
                using (var writer = new StreamWriter(fileStream))
                {
                    await writer.WriteAsync(text);
                }
            }
        }

        /// <inheritdoc/>
        public bool FileExists(string path)
        {
            return File.Exists(path);
        }
    }
}