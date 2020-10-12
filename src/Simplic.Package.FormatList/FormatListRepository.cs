using Dapper;
using Simplic.Sql;
using System;
using System.Threading.Tasks;

namespace Simplic.Package.FormatList
{
    public class FormatListRepository : IObjectRepository
    {
        private readonly ISqlService sqlService;

        public FormatListRepository(ISqlService sqlService)
        {
            this.sqlService = sqlService;
        }

        public async Task<InstallObjectResult> InstallObject(InstallableObject installableObject)
        {
            if (installableObject.Content is FormatList formatList)
            {
                var result = new InstallObjectResult
                {
                    LogLevel = LogLevel.Info
                };

                try
                {
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
                    result.Message = $"Installed FormatList at {installableObject.Target}.";
                    result.Success = true;
                }
                catch (Exception ex)
                {
                    result.Message = $"Failed to install FormatList at {installableObject.Target}.";
                    result.LogLevel = LogLevel.Error;
                    result.Exception = ex;
                }
                return result;
            }
            throw new InvalidContentException();
        }

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