using Simplic.Sql;
using System;
using System.Threading.Tasks;

namespace Simplic.Package.Sql
{
    public class SqlRepository : IObjectRepository
    {
        private readonly ISqlService sqlService;

        public SqlRepository(ISqlService sqlService)
        {
            this.sqlService = sqlService;
        }

        public async Task<InstallObjectResult> InstallObject(InstallableObject installableObject)
        {
            if (installableObject.Content is SqlContent sqlContent)
            {
                var result = new InstallObjectResult
                {
                    LogLevel = LogLevel.Info
                };

                try
                {
                    await sqlService.OpenConnection(async (c) =>
                    {
                        var command = c.CreateCommand();
                        command.CommandText = sqlContent.Data;

                        command.ExecuteNonQuery();
                    });

                    result.Success = true;
                    result.Message = $"Succesfully executed sqlscript: {sqlContent.Data}\n at {installableObject.Target}.";
                }
                catch (Exception ex)
                {
                    result.Message = $"Failed to execute sqlscript:{sqlContent.Data}\n at {installableObject.Target}.";
                    result.LogLevel = LogLevel.Error;
                    result.Exception = ex;
                }
                return result;
            }
            throw new InvalidContentException("");
        }

        public Task<UninstallObjectResult> UninstallObject(InstallableObject installableObject)
        {
            throw new NotImplementedException();
        }
    }
}