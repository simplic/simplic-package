using Simplic.Sql;
using System;
using System.Threading.Tasks;

namespace Simplic.Package.Sql
{
    public class SqlRepository : IObjectRepository
    {
        private readonly ISqlService sqlService;
        private readonly ILogService logService;

        public SqlRepository(ISqlService sqlService, ILogService logService)
        {
            this.sqlService = sqlService;
            this.logService = logService;
        }

        public async Task<InstallObjectResult> InstallObject(InstallableObject installableObject)
        {
            if (installableObject.Content is SqlContent sqlContent)
            {
                var result = new InstallObjectResult { Success = true };

                try
                {
                    await sqlService.OpenConnection(async (c) =>
                    {
                        var command = c.CreateCommand();
                        command.CommandText = sqlContent.Data;

                        command.ExecuteNonQuery();
                    });

                    result.Success = true;
                    await logService.WriteAsync($"Succesfully executed sqlscript: {sqlContent.Data}\n at {installableObject.Target}.", LogLevel.Info);
                }
                catch (Exception ex)
                {
                    await logService.WriteAsync($"Failed to execute sqlscript:{sqlContent.Data}\n at {installableObject.Target}.", LogLevel.Error, ex);

                    result.Success = false;
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