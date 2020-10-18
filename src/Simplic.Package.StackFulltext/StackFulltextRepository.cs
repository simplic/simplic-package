using Dapper;
using Simplic.Sql;
using System;
using System.Threading.Tasks;

namespace Simplic.Package.StackFulltext
{
    public class StackFulltextRepository : IObjectRepository
    {
        private readonly ISqlService sqlService;
        private readonly ILogService logService;

        public StackFulltextRepository(ISqlService sqlService, ILogService logService)
        {
            this.sqlService = sqlService;
            this.logService = logService;
        }

        public async Task<InstallObjectResult> InstallObject(InstallableObject installableObject)
        {
            if (installableObject.Content is StackFulltext stackFulltext)
            {
                var result = new InstallObjectResult { Success = true };

                try
                {
                    var sqlStatement = "";
                    if (stackFulltext.Configuration is SqlConfiguration sqlConfiguration)
                        sqlStatement = sqlConfiguration.Statement;

                    result.Success = await sqlService.OpenConnection(async (c) =>
                    {
                        var affectedRows = await c.ExecuteAsync("Insert into Stack_ContentExtractionSql (guid, stackguid, sql) on existing update values (:id, :stackid, :sqlstatement)",
                                                            new { stackFulltext.Id, stackFulltext.StackId, sqlStatement });
                        return affectedRows > 0;
                    });

                    if (result.Success)
                    {
                        await logService.WriteAsync($"Installed StackFulltext at {installableObject.Target}.", LogLevel.Info);
                    }
                    else
                    {
                        await logService.WriteAsync($"Failed to install StackFulltext at {installableObject.Target}.", LogLevel.Warning);
                    }
                }
                catch (Exception ex)
                {
                    await logService.WriteAsync($"Failed to install StackFulltext at {installableObject.Target}.", LogLevel.Error, ex);

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