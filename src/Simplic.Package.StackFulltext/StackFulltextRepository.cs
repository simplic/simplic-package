using Dapper;
using Simplic.Sql;
using System;
using System.Threading.Tasks;

namespace Simplic.Package.StackFulltext
{
    public class StackFulltextRepository : IObjectRepository
    {
        private readonly ISqlService sqlService;

        public StackFulltextRepository(ISqlService sqlService)
        {
            this.sqlService = sqlService;
        }

        public async Task<InstallObjectResult> InstallObject(InstallableObject installableObject)
        {
            if (installableObject.Content is StackFulltext stackFulltext)
            {
                var result = new InstallObjectResult
                {
                    LogLevel = LogLevel.Info
                };

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
                        result.Message = $"Installed StackFulltext at {installableObject.Target}.";
                    else
                    {
                        result.Message = $"Failed to install StackFulltext at {installableObject.Target}.";
                        result.LogLevel = LogLevel.Error;
                    }
                }
                catch (Exception ex)
                {
                    result.Message = $"Failed to install StackFulltext at {installableObject.Target}.";
                    result.LogLevel = LogLevel.Error;
                    result.Exception = ex;
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