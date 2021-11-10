using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace Simplic.Package.Icon
{
    /// <summary>
    /// Service to unpack an icon.
    /// </summary>
    public class UnpackIconService : IUnpackObjectService
    {
        /// <inheritdoc/>
        public async Task<UnpackObjectResult> UnpackObject(ExtractArchiveEntryResult extractArchiveEntryResult)
        {
            var filename = Path.GetFileNameWithoutExtension(extractArchiveEntryResult.Location);

            var installableObject = new InstallableObject
            {
                Content = new IconContent
                {
                    Guid = Guid.Parse(filename.Split('#').First()),
                    Name = filename.Split('#').Last(),
                    Blob = extractArchiveEntryResult.Data
                },
                Target = extractArchiveEntryResult.Location,
                Mode = extractArchiveEntryResult.Mode
            };

            return new UnpackObjectResult
            {
                InstallableObject = installableObject,
                Message = $"Unpacked icon at {extractArchiveEntryResult.Location}.",
                LogLevel = LogLevel.Info
            };
        }
    }
}