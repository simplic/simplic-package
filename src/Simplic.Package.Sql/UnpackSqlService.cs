using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package.Sql
{
    /// <summary>
    /// Service to unpack sql exports.
    /// </summary>
    public class UnpackSqlService : IUnpackObjectService
    {
        /// <inheritdoc/> 
        public async Task<UnpackObjectResult> UnpackObject(ExtractArchiveEntryResult extractArchiveEntryResult)
        {
            var installableObject = new InstallableObject
            {
                Content = new SqlContent
                {
                    Data = Encoding.Default.GetString(extractArchiveEntryResult.Data)
                },
                Target = extractArchiveEntryResult.Location,
                Mode = extractArchiveEntryResult.Mode
            };

            return new UnpackObjectResult
            {
                InstallableObject = installableObject,
                Message = $"Unpacked sql file at {extractArchiveEntryResult.Location}.",
                LogLevel = LogLevel.Info
            };
        }
    }
}