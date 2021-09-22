using Dapper;
using Simplic.Sql;
using System;
using System.Threading.Tasks;

namespace Simplic.Package.StackRegister
{
    /// <summary>
    /// Repository to read and write stack register to the database.
    /// </summary>
    public class StackRegisterRepository : IObjectRepository
    {
        private readonly ISqlService sqlService;
        private readonly ILogService logService;

        /// <summary>
        /// Initializes a new instance of <see cref="StackRegisterRepository"/>.
        /// </summary>
        /// <param name="sqlService"></param>
        /// <param name="logService"></param>
        public StackRegisterRepository(ISqlService sqlService, ILogService logService)
        {
            this.sqlService = sqlService;
            this.logService = logService;
        }

        /// <inheritdoc/>
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
                        var affectedRows = await c.ExecuteAsync(
                            "INSERT INTO ESS_DCC_StackRegister (" +
                            "RegisterGuid, RegisterName, StackGuid, IsActive, AssignSql) " +
                            "ON EXISTING UPDATE VALUES (" +
                            ":Id, :Name, :Stackid, :IsActive, :Statement)",
                             new
                             {
                                 stackRegister.Id,
                                 stackRegister.Name,
                                 stackRegister.StackId,
                                 stackRegister.IsActive,
                                 statement
                             });
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

        /// <inheritdoc/>
        public Task<UninstallObjectResult> UninstallObject(InstallableObject installableObject)
        {
            throw new NotImplementedException();
        }
    }
}