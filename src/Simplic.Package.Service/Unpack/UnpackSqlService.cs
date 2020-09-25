using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package.Service.Unpack
{
    public class UnpackSqlService : IUnpackObjectService
    {
        // Sql Files should not be deserialized
        public InstallableObject DeserializeObject(UnpackObjectResult unpackObjectResult)
        {
            throw new NotImplementedException();
        }


        public InstallableObject UnpackObject(UnpackObjectResult unpackObjectResult)
        {
            var sqlContent = new SqlContent
            {
                Data = unpackObjectResult.Data
            };

            return new InstallableObject
            {
                Content = sqlContent,
                Target = unpackObjectResult.Location
            };
        }
    }
}
