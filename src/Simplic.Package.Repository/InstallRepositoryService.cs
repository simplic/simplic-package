using Simplic.Framework.Repository;
using System;
using System.Threading.Tasks;
using Unity;

namespace Simplic.Package.Repository
{
    public class InstallRepositoryService : IInstallObjectService
    {
        public async Task<InstallObjectResult> InstallObject(InstallableObject installableObject)
        {
            if (installableObject.Content is RepositoryContent repositoryContent)
            {
                var repositoryManager = RepositoryManager.Singleton;
                var installObjectResult = new InstallObjectResult
                {
                    LogLevel = LogLevel.Info,
                };

                try
                {
                    repositoryManager.WriteAllBytes(installableObject.Target, repositoryContent.Data); // TODO: System.IO.FileNotFoundException: Could not find file in repository.
                    // bei Simplic.Framework.Repository.RepositoryManager.ReadAllBytes(String path)
                    installObjectResult.Success = true;
                    installObjectResult.Message = $"Installed repository content at {installableObject.Target}.";
                }
                catch (Exception ex)
                {
                    installObjectResult.Message = $"Failed to install repository content at {installableObject.Target}.";
                    installObjectResult.LogLevel = LogLevel.Error;
                    installObjectResult.Exception = ex;
                }
                return installObjectResult;
            }
            throw new InvalidContentException();
        }

        public Task OverwriteObject(InstallableObject installableObject)
        {
            throw new NotImplementedException();
        }

        public Task<UninstallObjectResult> UninstallObject(InstallableObject installableObject)
        {
            throw new NotImplementedException();
        }
    }
}