using Dapper;
using Simplic.Sql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package.Data.DB
{
    public class SqlRepository : IObjectRepository
    {
        private readonly ISqlService sqlService;

        public SqlRepository(ISqlService sqlService)
        {
            this.sqlService = sqlService;
        }

        public async Task<CheckMigrationResult> CheckMigration(InstallableObject installableObject)
        {
            var checkMigrationResult = await sqlService.OpenConnection<Task<CheckMigrationResult>>(async (c) =>
            {
                var result = new CheckMigrationResult();

                var row = await c.QueryFirstOrDefaultAsync("Select * from Package_Object where object_type = 'sql' and target = :target", new { installableObject.Target });
                
                if (row != null)
                {
                    result.CanMigrate = false;
                    result.LogMessage = $"The sql script @ {installableObject.Target} wont be migrated as it was already executed.";
                    result.LogLevel = LogLevel.Info;
                }
                else
                {
                    result.CanMigrate = true;
                    result.LogMessage = $"The sql script @ {installableObject.Target} will be migrated";
                    result.LogLevel = LogLevel.Info;
                }
                return result;
            });

            return checkMigrationResult;
        }

        public async Task<InstallObjectResult> InstallObject(InstallableObject installableObject)
        {
            if (installableObject.Content is SqlContent sqlContent)
            {
                var installObjectResult = await sqlService.OpenConnection<Task<InstallObjectResult>>(async (c) =>
                {
                    var result = new InstallObjectResult { Success = true };
                    try
                    {
                        await c.ExecuteAsync(sqlContent.Data);
                        result.LogMessage = $"Succesfully executed {installableObject.Content}!";
                        result.LogLevel = LogLevel.Info;
                    }
                    catch (Exception ex)
                    {
                        result.Success = false;
                        result.Exception = ex;
                        result.LogMessage = $"Failed to execute {installableObject.Content}!";
                        result.LogLevel = LogLevel.Error;
                    }

                    if (result.Success)
                    {
                        try
                        {
                            await c.ExecuteAsync("Insert into Package_Object (object_type, target) values ('sql', :target)", new { installableObject.Target });
                            result.LogMessage = $"Succesfully executed and added to Package_Object table! Target:{installableObject.Target}, content:{installableObject.Content}!";
                            result.LogLevel = LogLevel.Info;
                        }
                        catch (Exception ex)
                        {
                            result.Success = false;
                            result.Exception = ex;
                            result.LogMessage = $"Executed but failed to add to Package_Object table! Target:{installableObject.Target}, content:{installableObject.Content}!";
                            result.LogLevel = LogLevel.Error;
                        }
                    }

                    return result;
                });
                return installObjectResult;
            }
            throw new InvalidObjectException($"The content of of {installableObject.Target} was not of Type SqlContent!");
        }
    }
}
