using System.Linq;
using System.Threading.Tasks;

namespace Simplic.Package.Icon
{
    public class UnpackIconService : IUnpackObjectService
    {
        public async Task<UnpackObjectResult> UnpackObject(ExtractArchiveEntryResult extractArchiveEntryResult)
        {
            var installableObject = new InstallableObject
            {
                Content = new IconContent
                {
                    Name = extractArchiveEntryResult.Location.Split('/').Last(),
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