using System;
using System.Threading.Tasks;

namespace Simplic.Package.Stack
{
    public class StackRepository : IObjectRepository
    {
        public Task<CheckMigrationResult> CheckMigration(InstallableObject installableObject)
        {
            throw new NotImplementedException();
        }

        public Task<InstallObjectResult> InstallObject(InstallableObject installableObject)
        {
            if (installableObject.Content is DeserializedStack stack)
            {
                var result = new InstallObjectResult
                {
                    LogLevel = LogLevel.Info
                };
                throw new NotImplementedException("A service for saving stacks will be created in the framework.");
            }
            throw new InvalidContentException();
        }

        public Task<UninstallObjectResult> UninstallObject(InstallableObject installableObject)
        {
            throw new NotImplementedException();
        }
    }
}