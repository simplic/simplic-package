using Simplic.Framework.Repository;
using System;
using System.Threading.Tasks;

namespace Simplic.Package.Repository
{
    /// <summary>
    /// Service to install reprository items.
    /// </summary>
    public class InstallRepositoryService : IInstallObjectService
    {
        private readonly ILogService logService;

        /// <summary>
        /// Initializes a new instance of <see cref="InstallRepositoryService"/>.
        /// </summary>
        /// <param name="logService"></param>
        public InstallRepositoryService(ILogService logService)
        {
            this.logService = logService;
        }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public Task<UninstallObjectResult> UninstallObject(InstallableObject installableObject)
        {
            throw new NotImplementedException();
        }
    }
}