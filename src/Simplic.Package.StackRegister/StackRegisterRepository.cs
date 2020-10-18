using Dapper;
using Simplic.Sql;
using System;
using System.Threading.Tasks;

namespace Simplic.Package.StackRegister
{
    public class StackRegisterRepository : IObjectRepository
    {
        private readonly ISqlService sqlService;
        private readonly ILogService logService;

        public StackRegisterRepository(ISqlService sqlService, ILogService logService)
        {
            this.sqlService = sqlService;
            this.logService = logService;
        }

        public async Task<InstallObjectResult> InstallObject(InstallableObject installableObject)
        {
            if (installableObject.Content is StackRegister stackRegister)
            {
                var result = new InstallObjectResult { Success = true };

                try
                {
                    var statement = "";
                    if (stackRegister.Configuration is SqlConfiguration sqlConfiguration)
                        statement = sqlConfiguration.Statement;

                    var success = await sqlService.OpenConnection(async (c) =>
                    {
                        var affectedRows = await c.ExecuteAsync("Insert into ESS_DCC_StackRegister (registerguid, registername, stackguid, isactive, assignsql) " +
                                                                "on existing update values (:id, :name, :stackid, :isactive, :statement)",
                                                                new { stackRegister.Id, stackRegister.Name, stackRegister.StackId, stackRegister.IsActive, statement });
                        return affectedRows > 0;
                    });

                    if (success)
                    {
                        result.Success = true;
                        await logService.WriteAsync($"Installed StackRegister at {installableObject.Target}.", LogLevel.Info);
                    }
                    else
                    {
                        await logService.WriteAsync($"Failed to install StackRegister at {installableObject.Target}.", LogLevel.Warning);
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