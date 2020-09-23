using Newtonsoft.Json;
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
            // Deserialize Grid
            var bytes = await fileService.ReadAllBytesAsync(entry);
            var json = Encoding.Default.GetString(bytes);
            var deserializedGrid = JsonConvert.DeserializeObject<DeserializedGrid>(json);

            return new UnpackObjectResult
            {
                DeserializedContent = deserializedGrid
            };
        }
    }
}
