using Simplic.Framework.Repository;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Unity;

namespace Simplic.Package.Repository
{
    public class InstallRepositoryService : IInstallObjectService
    {
        private readonly ILogService logService;

        public InstallRepositoryService(ILogService logService)
        {
            this.logService = logService;
        }

        public async Task<InstallObjectResult> InstallObject(InstallableObject installableObject)
        {
            if (installableObject.Content is RepositoryContent repositoryContent)
            {
                var repositoryManager = RepositoryManager.Singleton;
                var result = new InstallObjectResult { Success = true };

                try
                {
                    repositoryManager.WriteAllBytes(installableObject.Target, repositoryContent.Data);

                    await logService.WriteAsync($"Installed repository content at {installableObject.Target}.", LogLevel.Info);
                }
                catch (Exception ex)
                {
                    await logService.WriteAsync($"Failed to install repository content at {installableObject.Target}.", LogLevel.Error, ex);

                    result.Success = false;
                }

                return result;
            }
            throw new InvalidContentException();
        }

        public Task<UninstallObjectResult> UninstallObject(InstallableObject installableObject)
        {
            throw new NotImplementedException();
        }
    }
}