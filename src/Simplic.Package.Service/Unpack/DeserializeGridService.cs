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
    public class DeserializeGridService : IDeserializeObjectService
    {
        private readonly IFileService fileService;
        public DeserializeGridService(IFileService fileService)
        {
            this.fileService = fileService;
        }
        public IDeserializedContent DeserializeObject(byte[] entryBytes)
        {
            var json = Encoding.Default.GetString(entryBytes);
            var deserializedGrid = JsonConvert.DeserializeObject<DeserializedGrid>(json);

            return deserializedGrid;
        }
    }
}
