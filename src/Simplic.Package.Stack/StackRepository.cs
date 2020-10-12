using Dapper;
using Simplic.Sql;
using System;
using System.Threading.Tasks;

namespace Simplic.Package.Stack
{
    public class StackRepository : IObjectRepository
    {
        private readonly ISqlService sqlService;

        public StackRepository(ISqlService sqlService)
        {
            this.sqlService = sqlService;
        }

        public async Task<InstallObjectResult> InstallObject(InstallableObject installableObject)
        {
            if (installableObject.Content is DeserializedStack stack)
            {
                var result = new InstallObjectResult
                {
                    LogLevel = LogLevel.Info
                };

                try
                {
                    result.Success = await sqlService.OpenConnection(async (c) =>
                    {
                        var affectedRows = await c.ExecuteAsync(
                            "Insert into ESS_DCC_Stack (guid, displayname, stackgridname, isactive, connectwitharchiv, tablename, stackname," +
                                                        " headersql, trackchanges, usefulltextindex, improveocrtext, usedce, stacksearchname)" +
                            "on existing update values (:id, :displayname, :stackgridname, :isactive, :connectwitharchive, :tablename, :stackname," +
                                                        " :headersql, :trackchanges, :usefulltextindex, :improveocrtext, :usedce, :stacksearchname)",
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
                                stack.FullText.UseDCE,
                                stack.StackSearchName
                            }
                            );

                        return affectedRows > 0;
                    });

                    if (result.Success)
                        result.Message = $"Installed stack at {installableObject.Target}.";
                    else
                    {
                        result.Message = $"Failed to install stack at {installableObject.Target}.";
                        result.LogLevel = LogLevel.Error;
                    }
                }
                catch (Exception ex)
                {
                    result.Message = $"Failed to install stack at {installableObject.Target}.";
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