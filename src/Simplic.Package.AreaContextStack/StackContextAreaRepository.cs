using Dapper;
using Simplic.Sql;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Simplic.Package.StackContextArea
{
    public class StackContextAreaRepository : IObjectRepository
    {
        private readonly ISqlService sqlService;

        public StackContextAreaRepository(ISqlService sqlService)
        {
            this.sqlService = sqlService;
        }

        public async Task<InstallObjectResult> InstallObject(InstallableObject installableObject)
        {
            if (installableObject.Content is StackContextArea stackContextArea)
            {
                var result = new InstallObjectResult
                {
                    LogLevel = LogLevel.Info
                };

                try
                {
                    var statement = $"Insert into IDContext (guid, displayname, stackguid, searchname) on existing update values (:Id, :DisplayName, :StackId, :SearchName)";
                    var param = new StatementParam
                    {
                        Id = stackContextArea.Id,
                        StackId = stackContextArea.StackId,
                        DisplayName = stackContextArea.DisplayName,
                        SearchName = stackContextArea.SearchName
                    };

                    if (stackContextArea.Configuration is GridConfiguration gridConfiguration)
                    {
                        statement = $"Insert into IDContext (guid, displayname, stackguid, searchname, gridname, isstackbased, usearchiv) " +
                                    $"on existing update values (:Id, :DisplayName, :StackId, :SearchName, :GridName, :StackBased, :ConnectWithArchive)";
                        param.ConnectWithArchive = gridConfiguration.ConnectWithArchive;
                        param.StackBased = gridConfiguration.StackBased;
                        param.GridName = gridConfiguration.Grid;
                    }

                    result.Success = await sqlService.OpenConnection(async (c) =>
                    {
                        var affectedRows = await c.ExecuteAsync(statement, param);
                        return affectedRows > 0;
                    });

                    foreach (var item in stackContextArea.ContextOfStacks)
                    {
                        var exists = await IdContextAssignmentExists(item.StackId, stackContextArea.Id);
                        Console.WriteLine(exists);
                        Console.WriteLine(item);
                        if (!exists)
                        {
                            await sqlService.OpenConnection(async (c) =>
                            {
                                await c.ExecuteAsync("Insert into IDContext_Assignment (guid, stackguid, idcontextguid) values (:newguid, :stackid, :id)",
                                                            new { newGuid = Guid.NewGuid(), item.StackId, stackContextArea.Id });
                            });
                        }
                    }

                    if (result.Success)
                        result.Message = $"Installed StackContextArea at {installableObject.Target}.";
                    else
                    {
                        result.Message = $"Failed to install StackContextArea at {installableObject.Target}.";
                        result.LogLevel = LogLevel.Error;
                    }
                }
                catch (Exception ex)
                {
                    result.Message = $"Failed to install StackContextArea at {installableObject.Target}.";
                    result.LogLevel = LogLevel.Error;
                    result.Exception = ex;
                }
                return result;
            }
            throw new InvalidContentException();
        }

        private async Task<bool> IdContextAssignmentExists(Guid stackGuid, Guid IdContextGuid)
        {
            return await sqlService.OpenConnection(async (c) =>
            {
                var first = await c.QueryFirstOrDefaultAsync("Select * from IDCOntext_Assignment where stackguid = :stackguid and idcontextguid = :idcontextguid"
                                                            , new { stackGuid, IdContextGuid});

                return first != null;
            });
        }

        public Task<UninstallObjectResult> UninstallObject(InstallableObject installableObject)
        {
            throw new NotImplementedException();
        }
    }
}