using System;
using System.IO;
using System.Threading.Tasks;

namespace Simplic.Package.IronPythonScript
{
    /// <summary>
    /// Service to install an iron python script.
    /// Or in this case execute, not install.
    /// </summary>
    public class InstallScriptService : IInstallObjectService
    {
        private readonly ILogService logService;

        public InstallScriptService(ILogService logService)
        {
            this.logService = logService;
        }

        /// <inheritdoc/>
        public async Task<InstallObjectResult> InstallObject(InstallableObject installableObject)
        {
            try
            {
                if (installableObject.Content is IronPythonScript script)
                {
                    PackagePythonDlrHost.Host.DefaultScope.Execute(script.Script);
                    var classname = Path.GetFileName(installableObject.Target);
                    var classInstance = PackagePythonDlrHost.Host.DefaultScope.CreateClassInstance(classname);

                    classInstance.Instance.execute();
                }
                return new InstallObjectResult { Success = true };
            }
            catch (Exception ex)
            {
                await logService.WriteAsync("Error during script execution", LogLevel.Error, ex);
                return new InstallObjectResult { Success = false };
            }
        }

        /// <inheritdoc/>
        public Task<UninstallObjectResult> UninstallObject(InstallableObject installableObject)
        {
            throw new NotImplementedException();
        }
    }
}
