using Simplic.Framework.Core;
using System;
using System.Threading.Tasks;

namespace Simplic.Package.Role
{
    /// <summary>
    /// Service to install a role.
    /// </summary>
    public class InstallRoleService : IInstallObjectService
    {
        private readonly ILogService logService;

        /// <summary>
        /// Initializes a new instance of <see cref="InstallRoleService"/>.
        /// </summary>
        /// <param name="logService"></param>
        public InstallRoleService(ILogService logService)
        {
            this.logService = logService;
        }

        /// <inheritdoc/>
        public async Task<InstallObjectResult> InstallObject(InstallableObject installableObject)
        {
            if (installableObject.Content is Role role)
            {
                var result = new InstallObjectResult { Success = true };

                try
                {
                    RoleManager.Singleton.CreateRole(new Simplic.Framework.EF.Role
                    {
                        RoleId = role.Id,
                        Description = role.Description,
                        DisplayName = role.DisplayName,
                        InternName = role.InternalName
                    });

                    await logService.WriteAsync($"Installed role at {installableObject.Target}.", LogLevel.Info);
                }
                catch (Exception ex)
                {
                    await logService.WriteAsync($"Failed to install role at {installableObject.Target}.", LogLevel.Error, ex);

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