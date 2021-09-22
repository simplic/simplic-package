using Dapper;
using Simplic.Sql;
using System;
using System.Threading.Tasks;

namespace Simplic.Package.FormatList
{
    /// <summary>
    /// Repository to check and write format lists to a database.
    /// </summary>
    public class FormatListRepository : IObjectRepository
    {
        private readonly ISqlService sqlService;
        private readonly ILogService logService;

        /// <summary>
        /// Initializes a new instance of <see cref="FormatListRepository"/>.
        /// </summary>
        /// <param name="sqlService"></param>
        /// <param name="logService"></param>
        public FormatListRepository(ISqlService sqlService, ILogService logService)
        {
            this.sqlService = sqlService;
            this.logService = logService;
        }

        /// <inheritdoc/>
        public async Task<InstallObjectResult> InstallObject(InstallableObject installableObject)
        {
            if (installableObject.Content is FormatList formatList)
            {
                var result = new InstallObjectResult { Success = true };

                try
                {
                    // TODO: Internal name  + entry value is unique
                    var updatedRows = 0;
                    foreach (var entry in formatList.Items)
                    {
                        updatedRows += await sqlService.OpenConnection(async (c) =>
                        {
                            return await c.ExecuteAsync("Insert into ESS_MS_Controls_FormatList (displayname, internname, descriptiontext, entryvalue) values " +
                                                        "(:displayname, :internalname, :description, :value);"
                                                        , new { entry.DisplayName, formatList.InternalName, formatList.Description, entry.Value });
                        });
                    }

                    await logService.WriteAsync($"Installed FormatList at {installableObject.Target}.", LogLevel.Info);
                }
                catch (Exception ex)
                {
                    await logService.WriteAsync($"Failed to install FormatList at {installableObject.Target}.", LogLevel.Error, ex);

                    result.Success = false;
                }
                return result;
            }

            throw new InvalidContentException();
        }

        /// <inheritdoc/>
        public async Task<UninstallObjectResult> UninstallObject(InstallableObject installableObject)
        {
            if (installableObject.Content is FormatList formatList)
            {
                var result = new UninstallObjectResult
                {
                    LogLevel = LogLevel.Info
                };

                try
                {
                    var deletedRows = await sqlService.OpenConnection(async (x) =>
                    {
                        // TODO: Just delete by internal name
                        return await x.ExecuteAsync("Delete from ESS_MS_Controls_FormatList");
                    });

                    result.Success = true;
                    result.Message = $"Deleted {deletedRows} FormatLists.";
                }
                catch (Exception ex)
                {
                    result.Message = $"Failed to delete FormatList at {installableObject.Target}.";
                    result.LogLevel = LogLevel.Error;
                    result.Exception = ex;
                }

                return result;
            }

            throw new InvalidContentException();
        }
    }
}