﻿using Dapper;
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
                    Debugger.Launch();
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

                    // TODO: What to put in IDContext_Assignment

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

        public Task<UninstallObjectResult> UninstallObject(InstallableObject installableObject)
        {
            throw new NotImplementedException();
        }
    }
}