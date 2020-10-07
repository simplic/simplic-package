using Dapper;
using Simplic.Sql;
using System;
using System.Threading.Tasks;

namespace Simplic.Package.StackRegister
{
    public class StackRegisterRepository : IObjectRepository
    {
        private readonly ISqlService sqlService;

        public StackRegisterRepository(ISqlService sqlService)
        {
            this.sqlService = sqlService;
        }

        public Task<CheckMigrationResult> CheckMigration(InstallableObject installableObject)
        {
            throw new NotImplementedException();
        }

        public async Task<InstallObjectResult> InstallObject(InstallableObject installableObject)
        {
            if (installableObject.Content is StackRegister stackRegister)
            {
                var result = new InstallObjectResult
                {
                    LogLevel = LogLevel.Info
                };

                try
                {
                    var sqlStatement = "";
                    if (stackRegister.Configuration is SqlConfiguration sqlConfiguration)
                        sqlStatement = sqlConfiguration.SqlStatement;

                    var success = await sqlService.OpenConnection(async (c) =>
                    {
                        var affectedRows = await c.ExecuteAsync("Insert into ESS_DCC_StackRegister (registerguid, registername, stackguid, isactive, assignsql) " +
                                                                "update on existing values (:id, :name, :stackid, :isactive, :sqlstatement)",
                                                                new { stackRegister.Id, stackRegister.Name, stackRegister.StackId, stackRegister.IsActive, sqlStatement });
                        return affectedRows > 0;
                    });

                    if (success)
                    {
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