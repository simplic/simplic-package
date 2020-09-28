using Newtonsoft.Json;
using System;
using System.Text;

namespace Simplic.Package.Service
{
    public class UnpackGridService : IUnpackObjectService
    {
        public InstallableObject UnpackObject(UnpackObjectResult unpackObjectResult, bool deserialize)
        {
            if (deserialize)
                return DeserializeObject(unpackObjectResult);
            else
                return CopyObject(unpackObjectResult);
        }

        public InstallableObject DeserializeObject(UnpackObjectResult unpackObjectResult)
        {
            var json = Encoding.Default.GetString(unpackObjectResult.Data);
            var deserializedGrid = JsonConvert.DeserializeObject<DeserializedGrid>(json, new JsonSerializerSettings { MissingMemberHandling = MissingMemberHandling.Error }); // TODO: try catch

            return new InstallableObject
            {
                Content = deserializedGrid,
                Target = unpackObjectResult.Location, // TODO: Has to point to database in some form (e.g. a table)
                Mode = unpackObjectResult.Mode
            };
        }

        public InstallableObject CopyObject(UnpackObjectResult unpackObjectResult)
        {
            throw new NotImplementedException("");
        }
    }
}