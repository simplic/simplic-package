using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package.Report
{
    public class PackReportService : PackObjectServiceBase, IPackObjectService
    {
        public PackReportService(IFileService fileService) : base(fileService)
        {
        }

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
