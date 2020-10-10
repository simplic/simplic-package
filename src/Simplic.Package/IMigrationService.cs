using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package
{
    public interface IMigrationService
    {
        Task<CheckMigrationResult> CheckMigration(InstallableObject installableObject);

        Task AddMigration(InstallableObject installableObject);
    }
}
