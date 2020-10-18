using Dapper;
using Simplic.Package.StackAutoconnector.Model;
using Simplic.Sql;
using System;
using System.Threading.Tasks;

namespace Simplic.Package.StackAutoconnector
{
    public class StackAutoconnectorRepository : IObjectRepository
    {
        private readonly ISqlService sqlService;
        private readonly ILogService logService;

        public StackAutoconnectorRepository(ISqlService sqlService, ILogService logService)
        {
            this.sqlService = sqlService;
            this.logService = logService;
        }

        public async Task<InstallObjectResult> InstallObject(InstallableObject installableObject)
        {
            if (installableObject.Content is StackAutoconnector stackAutoconnector)
            {
                var result = new InstallObjectResult { Success = true };

                try
                {
                    Guid? xmlId = null;
                    var xmlText = "";
                    var description = "";
                    if (stackAutoconnector.Configuration is XmlConfiguration xmlConfiguration)
                    {
                        xmlText = xmlConfiguration.Xml;
                        xmlId = xmlConfiguration.Id;
                        description = xmlConfiguration.Description;
                    }

                    var xmlSuccess = await sqlService.OpenConnection(async (c) =>
                    {
                        var affectedRows = await c.ExecuteAsync("Insert into ESS_DCC_SqlText (guid, sqltext, description) on existing update values (:xmlId, :xmlText, :description)",
                                                                new { xmlId, xmlText, description });
                        return affectedRows > 0;
                    });

                    if (xmlSuccess)
                    {

                        var success = await sqlService.OpenConnection(async (c) =>
                                    {
                                        var affectedRows = await c.ExecuteAsync("Insert into ESS_DCC_Stack_AutoConnect (sourcestackguid, name, tostackguid, xmlguid) " +
                                                                                "on existing update values (:stackid, :name, :target, :xmlid)",
                                                                                new { stackAutoconnector.StackId, stackAutoconnector.Name, stackAutoconnector.Target, xmlId });
                                        return affectedRows > 0;
                                    });

                        if (success)
                        {
                            await logService.WriteAsync($"Installed StackAutoconnector at {installableObject.Target}.", LogLevel.Info);
                        }
                        else
                        {
                            await logService.WriteAsync($"Installed xml to ESS_DCC_SqlText but failed to install StackAutoconnector at {installableObject.Target}.", LogLevel.Warning);
                        }
                    }
                    else
                    {
                        await logService.WriteAsync($"Failed to install StackAutoconnector at {installableObject.Target}.", LogLevel.Warning);
                    }
                }
                catch (Exception ex)
                {
                    await logService.WriteAsync($"Failed to install StackRegister at {installableObject.Target}.", LogLevel.Error, ex);

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