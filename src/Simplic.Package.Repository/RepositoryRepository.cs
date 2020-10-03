using Simplic.Framework.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package.Repository
{
    public class RepositoryRepository : IObjectRepository
    {
        public Task<CheckMigrationResult> CheckMigration(InstallableObject installableObject)
        {
        }

        public Task<InstallObjectResult> InstallObject(InstallableObject installableObject)
        {
            var repositoryManager = RepositoryManager.Singleton;
            var repositoryFileInfo = new RepositoryFileInfo(); // The content here can only be added via the the full path of the file.
        }
    }
}
