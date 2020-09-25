using System.IO;
using System.Threading.Tasks;

namespace Simplic.Package
{
    public interface IFileService
    {
        Task<byte[]> ReadAllBytesAsync(string path);

        Task<string> ReadAllTextAsync(string path);

        Task<byte[]> ReadAllBytesAsync(Stream stream);

        Task<string> ReadAllTextAsync(Stream stream);
    }
}