using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package.Data.DB
{
    public class SqlRepository : IObjectRepository
    {
        public Task<CheckMigrationResult> CheckMigration(InstallableObject installableObject)
        {
            throw new NotImplementedException();
        }

        public Task InstallObject(InstallableObject installableObject)
        {
            throw new NotImplementedException();
        }
    }
}
