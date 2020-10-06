using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package.StackAutoconnector
{
    public class StackAutoconnectorRepository : IObjectRepository
    {
        public Task<CheckMigrationResult> CheckMigration(InstallableObject installableObject)
        {
            throw new NotImplementedException();
        }

        public Task<InstallObjectResult> InstallObject(InstallableObject installableObject)
        {
            if (installableObject.Content is DeserializedStackAutoconnector stackAutoconnector)
            {
                var result = new InstallObjectResult
                {
                    LogLevel = LogLevel.Info
                };

                try
                {

                }
            }
            throw new InvalidContentException();
        }
    }
}
