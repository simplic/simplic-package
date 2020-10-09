using Dapper;
using Simplic.Sql;
using System;
using System.Threading.Tasks;

namespace Simplic.Package.Application
{
    public class ApplicationRepository : IObjectRepository
    {
        private readonly ISqlService sqlService;

        public ApplicationRepository(ISqlService sqlService)
        {
            this.sqlService = sqlService;
        }

        public Task<CheckMigrationResult> CheckMigration(InstallableObject installableObject)
        {
            throw new NotImplementedException();
        }

        public async Task<InstallObjectResult> InstallObject(InstallableObject installableObject)
        {
            if (installableObject.Content is DeserializedApplication application)
            {
                var result = new InstallObjectResult
                {
                    LogLevel = LogLevel.Info
                };

                try
                {
                    var contentTypeGuid = await GetContentTypeId(application.Type);
                    var success = await sqlService.OpenConnection(async (c) =>
                    {
                        var affectedRows = await c.ExecuteAsync("Insert into ESS_MS_Intern_Page (guid, iconguid, contenttype, menutext, directjump, ribbongroupguid)" +
                                                                " on existing update values (:Id, :IconId, :contentTypeGuid, :Name, :ShortCut, :RibbonGroupId)",
                                                                   new { application.Id, application.IconId, contentTypeGuid, application.Name,
                                                                        application.Shortcut, application.RibbonGroupId });
                        return affectedRows > 0;
                    });

                    if (success)
                    {
                        if (await SaveConfiguration(application, application.Type))
                        {
                            result.Success = true;
                            result.Message = $"Installed Application at {installableObject.Target}.";
                        }
                        else
                        {
                            result.Message = $"Installed Application but failed to install ApplicationConfiguration at {installableObject.Target}.";
                            result.LogLevel = LogLevel.Error;
                        }
                    }
                    else
                    {
                        result.Message = $"Failed to install Application at {installableObject.Target}.";
                        result.LogLevel = LogLevel.Warning;
                    }
                }
                catch (Exception ex)
                {
                    result.Message = $"Failed to install Application at {installableObject.Target}.";
                    result.LogLevel = LogLevel.Error;
                    result.Exception = ex;
                }
                return result;
            }
            throw new InvalidContentException();
        }

        private async Task<Guid> GetContentTypeId(string type)
        {
            // TODO: Which names
            var pageName = "";
            switch (type)
            {
                case "clr":
                    pageName = "Clr-Methode aufrufen";
                    break;
                case "python":
                    pageName = "Skriptübersicht";
                    break;
                case "grid":
                    pageName = "Einfache Auswahlliste";
                    break;
                case "grid-structure":
                    throw new NotImplementedException();
                case "browser":
                    pageName = "Browser";
                    break;
                default:
                    throw new Exception($"Invalid type {type} entered when trying to get ContentType from ESS_MS_Intern_Page_Content.");
            }

            return await sqlService.OpenConnection(async (c) =>
            {
                return await c.QueryFirstAsync<Guid>("Select * from ESS_MS_Intern_Page_Content where pagename = :pageName", new { pageName });
            });
        }

        private async Task<bool> SaveConfiguration(DeserializedApplication application, string type)
        {
            var configuration = application.Configuration;
            switch (type)
            {
                case "clr":
                    {
                        if (configuration is ClrConfiguration clrConfig)
                        {
                            var success = await sqlService.OpenConnection(async (c) =>
                            {
                                var affectedRows = await c.ExecuteAsync("Insert into Application_ClrCall (pageguid, clrnamespace, clrclass, clrmethod) " +
                                                                        "on existing update values (:id, :namespace, :class, :method)",
                                                                        new { application.Id, clrConfig.Namespace, clrConfig.Class, clrConfig.Method });
                                return affectedRows > 0;
                            });
                            return success;
                        }
                        throw new InvalidContentException("Type was specified to clr, but configuration was not of type ClrConfiguration");
                    }
                case "python":
                    {
                        if (configuration is PythonConfiguration pythonConfig)
                        {
                            var success = await sqlService.OpenConnection(async (c) =>
                            {
                                var affectedRows = await c.ExecuteAsync("Insert into ESS_MS_Intern_Page_Script (pageguid, scriptname, scriptmethod, classname) " +
                                                                        "on existing update values (:id, :path, :method, :class)",
                                                                        new { application.Id, pythonConfig.Path, pythonConfig.Method, pythonConfig.Class });
                                return affectedRows > 0;
                            });
                            return success;
                        }
                        throw new InvalidContentException("Type was specified to python, but configuration was not of type PythonConfiguration");
                    }
                case "grid":
                    {
                        if (configuration is GridConfiguration gridConfig)
                        {
                            var success = await sqlService.OpenConnection(async (c) =>
                            {
                                var affectedRows = await c.ExecuteAsync("Insert into ESS_MS_Intern_Page_DataGrid  (pageguid, gridname, loadonopen, dataconnectionstring) " +
                                                                        "on existing update values (:id, :grid, :loadonopen, :connection)",
                                                                        new { application.Id, gridConfig.Grid, gridConfig.LoadOnOpen, gridConfig.Connection, });
                                return affectedRows > 0;
                            });
                            return success;
                        }
                        throw new InvalidContentException("Type was specified to grid, but configuration was not of type GridConfiguration");
                    }
                case "browser":
                    {
                        if (configuration is BrowserConfiguration browserConfig)
                        {
                            var success = await sqlService.OpenConnection(async (c) =>
                            {
                                var affectedRows = await c.ExecuteAsync("Insert into Browser_Configuration (guid, tab, url) on existing update values (:id, :tag, :url) ",
                                                                        new { application.Id, browserConfig.Tab, browserConfig.Url });
                                return affectedRows > 0;
                            });
                            return success;
                        }
                        throw new InvalidContentException("Type was specified to python, but configuration was not of type PythonConfiguration");
                    }
                case "grid-structure":
                    {
                        if (configuration is GridStructureConfiguration config)
                        {
                            var stackAdded = 0;
                            foreach (var stack in config.Stacks)
                            {
                                stackAdded += await sqlService.OpenConnection(async (c) =>
                                {
                                    return await c.ExecuteAsync("Insert into ESS_DCC_Structure_Stack (guid, parentguid, stackguid, showstacknode, difgridname, difdisplayname, ordernr)" +
                                                                           " on existing update values (:id, :applicationid, :stackid, :isvisible, :grid, :displayname, :orderid)",
                                                                           new
                                                                           {
                                                                               stack.Id,
                                                                               applicationid = application.Id,
                                                                               stack.StackId,
                                                                               stack.IsVisible,
                                                                               stack.Grid,
                                                                               stack.DisplayName,
                                                                               stack.OrderId
                                                                           });
                                });

                                foreach (var register in stack.Registers)
                                {
                                    await sqlService.OpenConnection(async (c) =>
                                    {
                                        return await c.ExecuteAsync("Insert into ESS_DCC_Structure_Stack_Register (guid, parentguid, registerguid, difgridname, difdisplayname, ordernr)" +
                                                                               " on existing update values (:id, :stackid, :registerid, :grid, :displayname, :orderid)",
                                                                               new
                                                                               {
                                                                                   register.Id,
                                                                                   stack.StackId,
                                                                                   register.RegisterId,
                                                                                   register.Grid,
                                                                                   register.DisplayName,
                                                                                   register.OrderId
                                                                               });
                                    });
                                }
                            }
                            return stackAdded == config.Stacks.Count;
                        }
                        throw new InvalidContentException("Type was specified to grid-structure, but configuration was not of type GridStructureConfiguration");
                    }
                default:
                    throw new Exception($"Got unkown type for inserting configuration into database. Type: {type}.");
            }
        }

        public Task<UninstallObjectResult> UninstallObject(InstallableObject installableObject)
        {
            throw new NotImplementedException();
        }
    }
}