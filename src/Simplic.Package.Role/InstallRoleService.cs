using Simplic.Framework.Core;
using System;
using System.Threading.Tasks;
using Unity;

namespace Simplic.Package.Role
{
    public class InstallRoleService : IInstallObjectService
    {
        public async Task<InstallObjectResult> InstallObject(InstallableObject installableObject)
        {
            if (installableObject.Content is Role role)
            {
                var result = new InstallObjectResult
                {
                    LogLevel = LogLevel.Info
                };

                try
                {
                    // TODO:  Der Typeninitialisierer für "Simplic.Framework.Core.RoleManager" hat eine Ausnahme verursacht. ---> System.InvalidOperationException:  ServiceLocationProvider must be set.

                    RoleManager.Singleton.CreateRole(new Simplic.Framework.EF.Role
                    {
                        RoleId = role.Id,
                        Description = role.Description,
                        DisplayName = role.DisplayName,
                        InternName = role.InternalName
                    });

                    result.Message = $"Installed role at {installableObject.Target}.";
                    result.Success = true;
                }
                catch (Exception ex)
                {
                    result.Message = $"Failed to install role at {installableObject.Target}.";
                    result.LogLevel = LogLevel.Error;
                    result.Exception = ex;
                }

                return result;
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