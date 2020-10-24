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

        /// <summary>
        /// Writes bytes to a given path
        /// </summary>
        /// <param name="bytes">The bytes to write</param>
        /// <param name="path">The path to write to</param>
        Task WriteAllBytesAsync(byte[] bytes, string path);
        
        /// <summary>
        /// Writes bytes to a given path
        /// </summary>
        /// <param name="bytes">The stream whose content to write</param>
        /// <param name="path">The path to write to</param>
        Task WriteAllBytesAsync(Stream stream, string path);

        /// <summary>
        /// Writes text to a given path
        /// </summary>
        /// <param name="text">The text to write</param>
        /// <param name="path">The path to write to</param>
        Task WriteAllTextAsync(string text, string path);
        
        /// <summary>
        /// Checks whether a file exists at a given path
        /// </summary>
        /// <param name="path">The path to check</param>
        /// <returns>Whether the path exists</returns>
        bool FileExists(string path);
    }
}