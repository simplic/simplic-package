using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package.StackFulltext
{
    public class PackStackFulltextService : IPackObjectService
    {
        private readonly IFileService fileService;

        public PackStackFulltextService(IFileService fileService)
        {
            this.fileService = fileService;
        }

        public async Task<PackObjectResult> ReadAsync(ObjectListItem item)
        {
            return new PackObjectResult
            {
                File = await fileService.ReadAllBytesAsync(item.Source),
                Location = item.Target
            };
        }
    }
}
