using System.Threading.Tasks;

namespace Simplic.Package.Repository
{
    public class UnpackRepositoryService : IUnpackObjectService
    {
        public async Task<UnpackObjectResult> UnpackObject(ExtractArchiveEntryResult extractArchiveEntryResult)
        {
            var installableObject = new InstallableObject
            {
                Content = new RepositoryContent
                {
                    Data = extractArchiveEntryResult.Data
                },
                Target = extractArchiveEntryResult.Location, // TODO: This is just the path in the archive
                Mode = extractArchiveEntryResult.Mode
            };

            return new UnpackObjectResult
            {
                InstallableObject = installableObject,
                Message = $"Unpacked repository at {extractArchiveEntryResult.Location}",
                LogLevel = LogLevel.Info
            };
        }
    }
}