using Dapper;
using Simplic.Sql;
using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Simplic.Package.Stack
{
    public class StackRepository : IObjectRepository
    {
        private readonly ISqlService sqlService;
        private readonly ILogService logService;

        public StackRepository(ISqlService sqlService, ILogService logService)
        {
            this.sqlService = sqlService;
            this.logService = logService;
        }

        public async Task<InstallObjectResult> InstallObject(InstallableObject installableObject)
        {
            if (installableObject.Content is Stack stack)
            {
                var result = new InstallObjectResult { Success = true };

                try
                {
                    var execResult = await sqlService.OpenConnection(async (c) =>
                    {
                        var affectedRows = await c.ExecuteAsync(
                            "Insert into ESS_DCC_Stack (guid, displayname, stackgridname, isactive, connectwitharchiv, tablename, stackname," +
                                                        " headersql, trackchanges, usefulltextindex, improveocrtext, usedce, stacksearchname)" +
                            "on existing update values (:id, :displayname, :stackgridname, :isactive, :connectwitharchive, :tablename, :stackname," +
                                                        " :headersql, :trackchanges, :usefulltextindex, :improveocrtext, :usedce, '')",
                            new
                            {
                                stack.Id,
                                stack.DisplayName,
                                stack.StackGridName,
                                stack.IsActive,
                                stack.ConnectWithArchive,
                                stack.TableName,
                                stack.StackName,
                                stack.HeaderSql,
                                stack.TrackChanges,
                                stack.FullText.UseFullTextIndex,
                                stack.FullText.ImproveOCRText,
                                stack.FullText.UseDCE
                            }
                            );

                        return affectedRows > 0;
                    });

                    if (execResult)
                    {
                        await logService.WriteAsync($"Installed stack at {installableObject.Target}.", LogLevel.Info);
                    }
                    else
                    {
                        await logService.WriteAsync($"Failed to install stack at {installableObject.Target}.", LogLevel.Warning);
                    }
                }
                catch (Exception ex)
                {
                    await logService.WriteAsync($"Failed to install stack at {installableObject.Target}.", LogLevel.Error, ex);
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