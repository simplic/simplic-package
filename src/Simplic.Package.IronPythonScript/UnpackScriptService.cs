using System.Threading.Tasks;

namespace Simplic.Package.IronPythonScript
{
    /// <summary>
    /// Service to unpack script services
    /// </summary>
    public class UnpackScriptService : IUnpackObjectService
    {
        /// <inheritdoc/>
        public Task<UnpackObjectResult> UnpackObject(ExtractArchiveEntryResult extractArchiveEntryResult)
        {
            var installableObject = new InstallableObject
            {
                Content = new IronPythonScript
                {
                    Script = System.Text.Encoding.Default.GetString(extractArchiveEntryResult.Data)
                },
                Target = extractArchiveEntryResult.Location,
                Mode = extractArchiveEntryResult.Mode
            };

            return Task.FromResult(new UnpackObjectResult
            {
                InstallableObject = installableObject,
                Message = $"Unpacked repository at {extractArchiveEntryResult.Location}",
                LogLevel = LogLevel.Info
            });
        }
    }
}
