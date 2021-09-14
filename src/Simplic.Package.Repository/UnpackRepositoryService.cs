using System.Threading.Tasks;

namespace Simplic.Package.Repository
{
    /// <summary>
    /// Service to unpack a repository item.
    /// </summary>
    public class UnpackRepositoryService : IUnpackObjectService
    {
        /// <inheritdoc/>
        public async Task<UnpackObjectResult> UnpackObject(ExtractArchiveEntryResult extractArchiveEntryResult)
        {
            var installableObject = new InstallableObject
            {
                Content = new RepositoryContent
                {
                    Data = extractArchiveEntryResult.Data
                },
                Target = extractArchiveEntryResult.Location,
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