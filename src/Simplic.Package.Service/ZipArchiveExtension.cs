using System.IO;
using System.IO.Compression;
using System.Linq;

namespace Simplic.Package.Service
{
    /// <summary>
    /// Extension for <see cref="ZipArchive"/>.
    /// Found at stack overflow :
    /// https://stackoverflow.com/a/51514527
    /// </summary>
    public static class ZipArchiveExtension
    {
        /// <summary>
        /// Creates an entry from any source.
        /// </summary>
        /// <param name="archive">The zip archive instance.</param>
        /// <param name="sourceName">The source file/directory.</param>
        /// <param name="entryName">The target path</param>
        public static void CreateEntryFromAny(this ZipArchive archive, string sourceName, string entryName = "")
        {
            var fileName = Path.GetFileName(sourceName);
            if (File.GetAttributes(sourceName).HasFlag(FileAttributes.Directory))
            {
                archive.CreateEntryFromDirectory(sourceName, Path.Combine(entryName, fileName));
            }
            else
            {
                archive.CreateEntryFromFile(sourceName, Path.Combine(entryName, fileName), CompressionLevel.Fastest);
            }
        }

        /// <summary>
        /// Creates a directory from a source directory.
        /// </summary>
        /// <param name="archive">The zip archive.</param>
        /// <param name="sourceDirName">The source directory name.</param>
        /// <param name="entryName">The target directory path.</param>
        public static void CreateEntryFromDirectory(this ZipArchive archive, string sourceDirName, string entryName = "")
        {
            string[] files = Directory.GetFiles(sourceDirName).Concat(Directory.GetDirectories(sourceDirName)).ToArray();
            archive.CreateEntry(Path.Combine(entryName, Path.GetFileName(sourceDirName)));
            foreach (var file in files)
            {
                archive.CreateEntryFromAny(file, entryName);
            }
        }
    }
}
