﻿using Dapper;
using Simplic.Sql;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Simplic.Package.Application
{
    public class ApplicationRepository : IObjectRepository
    {
        private readonly ISqlService sqlService;
        private readonly ILogService logService;

        public ApplicationRepository(ISqlService sqlService, ILogService logService)
        {
            this.sqlService = sqlService;
            this.logService = logService;
        }

        public async Task<InstallObjectResult> InstallObject(InstallableObject installableObject)
        {
            if (installableObject.Content is Application application)
            {
                var result = new InstallObjectResult { Success = true };

                try
                {                    
                    var contentTypeGuid = GetContentTypeId(application.Type);
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
                            await logService.WriteAsync($"Installed Application at {installableObject.Target}.", LogLevel.Info);
                        }
                        else
                        {
                            result.Success = false;
                            
                            await logService.WriteAsync($"Installed Application but failed to install ApplicationConfiguration at {installableObject.Target}.", LogLevel.Error);
                        }
                    }
                    else
                    {
                        await logService.WriteAsync($"Failed to install Application at {installableObject.Target}.", LogLevel.Warning);
                    }
                }
                catch (Exception ex)
                {
                    result.Success = false;

                    await logService.WriteAsync($"Failed to install Application at {installableObject.Target}.", LogLevel.Error, ex);
                }
                return result;
            }
            throw new InvalidContentException();
        }

        private Guid GetContentTypeId(string type)
        {
            switch (type)
            {
                case "clr":
                    return Guid.Parse("7A1959FD-2F78-491E-BE38-1959DA826F8E");
                case "python":
                    return Guid.Parse("DE8D2FD3-7892-4D59-9936-EF8F2D7311E5");
                case "grid":
                    return Guid.Parse("6D29A4F2-C10C-4965-8527-19484FF50F63");
                case "grid-structure":
                    return Guid.Parse("0CFBB9C9-FBD5-44C4-AA29-4970B827F3D6");
                case "browser":
                    return Guid.Parse("92E3E5AE-A925-400E-83F3-E3D75615FCCF");
                default:
                    throw new Exception($"Invalid type {type} entered when trying to get ContentType from ESS_MS_Intern_Page_Content.");
            }
        }

        private async Task<bool> SaveConfiguration(Application application, string type)
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
                                var affectedRows = await c.ExecuteAsync("DELETE FROM ESS_MS_Intern_Page_DataGrid WHERE PageGuid = :id",
                                                                        new { application.Id });

                                affectedRows += await c.ExecuteAsync(" Insert into ESS_MS_Intern_Page_DataGrid (pageguid, gridname, loadonopen, dataconnectionstring, searchname) " +
                                                                        " on existing update values (:id, :grid, :loadonopen, :connection, :searchname)",
                                                                        new { id = application.Id, gridConfig.Grid, gridConfig.LoadOnOpen, gridConfig.Connection, gridConfig.SearchName });
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
                                var affectedRows = await c.ExecuteAsync("Insert into Browser_Configuration (pageguid, tabname, starturl) on existing update values (:id, :tab, :url) ",
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
                                    return await c.ExecuteAsync("Insert into ESS_DCC_Structure_Stack (guid, parentguid, stackguid, showstacknode, difgridname, difdisplayname, ordernr, difsearchname)" +
                                                                           " on existing update values (:id, :applicationid, :stackid, :isvisible, :grid, :displayname, :orderid, :searchname)",
                                                                           new
                                                                           {
                                                                               stack.Id,
                                                                               applicationid = application.Id,
                                                                               stack.StackId,
                                                                               stack.IsVisible,
                                                                               stack.Grid,
                                                                               stack.DisplayName,
                                                                               stack.OrderId,
                                                                               stack.SearchName
                                                                           });
                                });

                                foreach (var register in stack.Registers)
                                {
                                    await sqlService.OpenConnection(async (c) =>
                                    {
                                        return await c.ExecuteAsync("Insert into ESS_DCC_Structure_Stack_Register (guid, parentguid, registerguid, difgridname, difdisplayname, ordernr, difsearchname)" +
                                                                               " on existing update values (:id, :stackid, :registerid, :grid, :displayname, :orderid, :searchname)",
                                                                               new
                                                                               {
                                                                                   register.Id,
                                                                                   stack.StackId,
                                                                                   register.RegisterId,
                                                                                   register.Grid,
                                                                                   register.DisplayName,
                                                                                   register.OrderId,
                                                                                   register.SearchName
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