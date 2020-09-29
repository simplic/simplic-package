using Newtonsoft.Json;
using System;
using System.Text;

namespace Simplic.Package.Service
{
    public class UnpackGridService : IUnpackObjectService
    {
        public InstallableObject UnpackObject(UnpackObjectResult unpackObjectResult)
        {
            var json = Encoding.Default.GetString(unpackObjectResult.Data);
            var deserializedGrid = JsonConvert.DeserializeObject<DeserializedGrid>(json, new JsonSerializerSettings { MissingMemberHandling = MissingMemberHandling.Error }); // TODO: try catch

            return new InstallableObject
            {
                Content = deserializedGrid,
                Target = unpackObjectResult.Location, // TODO: is this even needed?
                Mode = unpackObjectResult.Mode
            };
        }
    }
}