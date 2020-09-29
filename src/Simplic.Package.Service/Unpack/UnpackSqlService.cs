using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package.Service.Unpack
{
    public class UnpackSqlService : IUnpackObjectService
    {
        public InstallableObject UnpackObject(UnpackObjectResult unpackObjectResult)
        {
            var sqlContent = new SqlContent
            {
                Data = Encoding.Default.GetString(unpackObjectResult.Data)
            };

            return new InstallableObject
            {
                Content = sqlContent,
                Target = unpackObjectResult.Location, // TODO:
                Mode = unpackObjectResult.Mode
            };
        }
    }
}
