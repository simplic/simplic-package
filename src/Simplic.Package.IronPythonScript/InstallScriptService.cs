using Simplic.Framework.Core;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Simplic.Package.IronPythonScript
{
    /// <summary>
    /// Service to install an iron python script.
    /// </summary>
    public class InstallScriptService : IInstallObjectService
    {
        /// <inheritdoc/>
        public Task<InstallObjectResult> InstallObject(InstallableObject installableObject)
        {
            try
            {
                if (installableObject.Content is IronPythonScript script)
                {
                    GlobalDlrHost.Host.DefaultScope.Execute(script.Script);
                    var classname = Path.GetFileName(installableObject.Target);
                    var classInstance = GlobalDlrHost.Host.DefaultScope.CreateClassInstance(classname);

                    classInstance.Instance.execute();
                }
                return Task.FromResult(new InstallObjectResult { Success = true });
            }
            catch
            {
                return Task.FromResult(new InstallObjectResult { Success = false });
            }
        }

        /// <inheritdoc/>
        public Task<UninstallObjectResult> UninstallObject(InstallableObject installableObject)
        {
            throw new NotImplementedException();
        }
    }
}
