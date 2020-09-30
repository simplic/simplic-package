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

        public Task<CheckMigrationResult> CheckMigration(InstallableObject installableObject)
        {
            throw new NotImplementedException();
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
                    return result;
                });
                return installObjectResult;
            }
            throw new InvalidObjectException($"The content of of {installableObject.Target} was not of Type SqlContent!");
        }
    }
}
