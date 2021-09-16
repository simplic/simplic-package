using Dapper;
using Simplic.Sql;
using System;
using System.Threading.Tasks;

namespace Simplic.Package.StackFulltext
{
    /// <summary>
    /// Repository to read and write sack fulltexts.
    /// </summary>
    public class StackFulltextRepository : IObjectRepository
    {
        private readonly ISqlService sqlService;
        private readonly ILogService logService;

        /// <summary>
        /// Initializes a new instance of <see cref="StackFulltextRepository"/>.
        /// </summary>
        /// <param name="sqlService"></param>
        /// <param name="logService"></param>
        public StackFulltextRepository(ISqlService sqlService, ILogService logService)
        {
            this.sqlService = sqlService;
            this.logService = logService;
        }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public Task<UninstallObjectResult> UninstallObject(InstallableObject installableObject)
        {
            throw new NotImplementedException();
        }
    }
}