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

        public StackAutoconnectorRepository(ISqlService sqlService)
        {
            this.sqlService = sqlService;
        }

        public Task<CheckMigrationResult> CheckMigration(InstallableObject installableObject)
        {
            throw new NotImplementedException();
        }

        public async Task<InstallObjectResult> InstallObject(InstallableObject installableObject)
        {
            if (installableObject.Content is DeserializedStackAutoconnector stackAutoconnector)
            {
                var result = new InstallObjectResult
                {
                    LogLevel = LogLevel.Info
                };

                try
                {
                    var xmlText = "";
                    Guid? xmlId = null;
                    if (stackAutoconnector.Configuration is XmlConfiguration xmlConfiguration)
                    {
                        xmlText = xmlConfiguration.Xml;
                        xmlId = xmlConfiguration.Id;
                    }

                    var success = await sqlService.OpenConnection(async (c) =>
                    {
                        var affectedRows = await c.ExecuteAsync("Insert into ESS_DCC_Stack_AutoConnect (sourcestackguid, name, tostackguid, xmlguid) " +
                                                                "update on existing values (:stackid, :name, :target, :xmlid)",
                                                                new { stackAutoconnector.StackId, stackAutoconnector.Name, stackAutoconnector.Target, xmlId });
                        return affectedRows > 0;
                    });

                    if (success)
                    {
                        and if xmlText and then insert into some xml table
                        result.Success = true;
                        result.Message = $"Installed StackRegister at {installableObject.Target}.";
                    }
                    else
                    {
                        result.Message = $"Failed to install StackRegister at {installableObject.Target}.";
                        result.LogLevel = LogLevel.Warning;
                    }
                }
                catch (Exception ex)
                {
                    result.Message = $"Failed to install StackRegister at {installableObject.Target}.";
                    result.LogLevel = LogLevel.Error;
                    result.Exception = ex;
                }
            }
            throw new InvalidContentException();
        }

        public Task<UninstallObjectResult> UninstallObject(InstallableObject installableObject)
        {
            throw new NotImplementedException();
        }
    }
}