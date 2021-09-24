using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Simplic.Package.Icon
{
    /// <summary>
    /// Service to pack an icon.
    /// </summary>
    public class PackIconService : IPackObjectService
    {
        private readonly IFileService fileService;

        /// <summary>
        /// Initializes a new instance of <see cref="PackIconService"/>.
        /// </summary>
        /// <param name="fileService"></param>
        public PackIconService(IFileService fileService)
        {
            this.fileService = fileService;
        }

        /// <summary>
        /// Packs a ObjectListItem into a PackObjectResult
        /// </summary>
        /// <param name="item">The ObjectListItem</param>
        /// <returns>A PackObjectResult object</returns>
        public virtual async Task<PackObjectResult> ReadAsync(ObjectListItem item)
        {
            if (!item.Target.Contains("#"))
                throw new FormatException($"Icon target path does not contain '#' in {item.Target}");

            if (!Guid.TryParse(Path.GetFileName(item.Target).Split('#').First(), out _))
                throw new FormatException($"Icon target path does not contain a valid guid in {item.Target}");

            if (Path.GetFileName(item.Target).Split('#').Last().Split('.').First().Length == 0)
                throw new FormatException($"Icon target path does not contain a valid filename in {item.Target}");

            return new PackObjectResult
            {
                File = await fileService.ReadAllBytesAsync(item.Source),
                Location = item.Target,
            };
        }
    }
}