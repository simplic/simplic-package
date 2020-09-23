using Newtonsoft.Json;
using Simplic.Package.Model;
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
        public IDeserializedContent DeserializeObject(UnpackObjectResult unpackObjectResult)
        {
            var json = Encoding.Default.GetString(unpackObjectResult.ReadBytes);
            var deserializedGrid = JsonConvert.DeserializeObject<DeserializedGrid>(json);

            return deserializedGrid;
        }
    }
}
