using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package.Service.Unpack
{
    public class UnpackSqlService : IUnpackObjectService
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
            throw new NotImplementedException();
        }

        public InstallableObject CopyObject(UnpackObjectResult unpackObjectResult)
        {
            var sqlContent = new SqlContent
            {
                Data = unpackObjectResult.Data
            };

            return new InstallableObject
            {
                Content = sqlContent,
                Target = unpackObjectResult.Location,
                Mode = unpackObjectResult.Mode
            };
        }
    }
}
