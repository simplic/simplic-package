using System.Collections.Generic;
using System.Threading.Tasks;

namespace Simplic.Package.Report
{
    /// <summary>
    /// Service to pack a result.
    /// </summary>
    public class PackReportService : PackObjectServiceBase, IPackObjectService
    {

        /// <summary>
        /// Initializes a enw isntance of <see cref="PackReportService"/>.
        /// </summary>
        /// <param name="fileService"></param>
        public PackReportService(IFileService fileService) : base(fileService)
        {
        }

        /// <inheritdoc/>
        public override async Task<PackObjectResult> ReadAsync(ObjectListItem item)
        {
            var result = new PackObjectResult
            {
                File = await fileService.ReadAllBytesAsync(item.Source),
                Location = item.Target,
                Payload = new Dictionary<string, byte[]>()
            };

            foreach (var payload in item.Payload)
                result.Payload[payload.Target] = await fileService.ReadAllBytesAsync(payload.Source);

            return result;
        }
    }
}