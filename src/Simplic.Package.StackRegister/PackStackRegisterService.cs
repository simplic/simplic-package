using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package.StackRegister
{
    public class PackStackRegisterService :IPackObjectService
    {
        private readonly IFileService fileService;

        public PackStackRegisterService(IFileService fileService)
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
