using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package.Service
{
    public class UnpackGridService : IUnpackObjectService
    {
        private readonly IFileService fileService;
        public UnpackGridService(IFileService fileService)
        {
            this.fileService = fileService;
        }
        public async Task<UnpackObjectResult> UnpackObject(ZipArchiveEntry entry)
        {
            return new UnpackObjectResult
            {
                SerializedContent = await fileService.ReadAllBytesAsync(entry)
            };
        }
    }
}
