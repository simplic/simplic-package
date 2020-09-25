using Newtonsoft.Json;
using System;
using System.Text;

namespace Simplic.Package.Service
{
    public class UnpackGridService : IUnpackObjectService
    {
        private readonly IFileService fileService;

        public UnpackGridService(IFileService fileService)
        {
            this.fileService = fileService;
        }

        public InstallableObject DeserializeObject(UnpackObjectResult unpackObjectResult)
        {
            var json = Encoding.Default.GetString(unpackObjectResult.Data);
            var deserializedGrid = JsonConvert.DeserializeObject<DeserializedGrid>(json, new JsonSerializerSettings { MissingMemberHandling = MissingMemberHandling.Error }); // TODO: try catch

            return new InstallableObject
            {
                Content = deserializedGrid,
                Target = unpackObjectResult.Location // TODO: Has to point to database in some form (e.g. a table)
            };
        }

        // This should not be called, as grids should be deserialized
        public InstallableObject UnpackObject(UnpackObjectResult unpackObjectResult)
        {
            throw new NotImplementedException();
        }
    }
}