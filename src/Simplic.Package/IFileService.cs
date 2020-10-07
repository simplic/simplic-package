using System.IO;
using System.Threading.Tasks;

namespace Simplic.Package
{
    /// <summary>
    /// Service for file IO operations
    /// </summary>
    public interface IFileService
    {
        /// <summary>
        /// Reads all bytes from a file at a given path
        /// </summary>
        /// <param name="path">The path to the file</param>
        /// <returns>The read bytes as an array</returns>
        Task<byte[]> ReadAllBytesAsync(string path);

        /// <summary>
        /// Reads all text from a file at a given path
        /// </summary>
        /// <param name="path">The path to the file</param>
        /// <returns>The read text as a string</returns>
        Task<string> ReadAllTextAsync(string path);

        /// <summary>
        /// Reads all bytes from a stream
        /// </summary>
        /// <param name="stream">The stream to read from</param>
        /// <returns>The read bytes as an array</returns>
        Task<byte[]> ReadAllBytesAsync(Stream stream);

        /// <summary>
        /// Reads all text from a stream
        /// </summary>
        /// <param name="stream">The stream to read from</param>
        /// <returns>The read text as a string</returns
        Task<string> ReadAllTextAsync(Stream stream);

        Task WriteAllBytesAsync(byte[] bytes, string path);

        Task WriteAllBytesAsync(Stream stream, string path);

        Task WriteAllTextAsync(string text, string path);
        
        bool FileExists(string path);
    }
}