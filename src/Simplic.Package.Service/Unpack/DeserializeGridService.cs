using Newtonsoft.Json;
using Simplic.Package;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package.Service
{
    public class DeserializeGridService : IUnpackObjectService
    {
        private readonly IFileService fileService;
        public DeserializeGridService(IFileService fileService)
        {
            this.fileService = fileService;
        }

        public InstallableObject DeserializeObject(UnpackObjectResult unpackObjectResult)
        {
            var json = Encoding.Default.GetString(unpackObjectResult.Data);
            var deserializedGrid = JsonConvert.DeserializeObject<DeserializedGrid>(json);

            return new InstallableObject
            {
                Content = deserializedGrid,
                Location = unpackObjectResult.Location
            };
        }

        // This should not be called, as grids should be deserialized
        public InstallableObject UnpackObject(UnpackObjectResult unpackObjectResult)
        {
            throw new NotImplementedException();
        }
    }
}
