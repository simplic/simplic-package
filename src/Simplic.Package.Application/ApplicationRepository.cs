using Dapper;
using Simplic.Sql;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Simplic.Package.Application
{
    /// <summary>
    /// Repository to load and check applications from the db.
    /// </summary>
    public class ApplicationRepository : IObjectRepository
    {
        private readonly ISqlService sqlService;
        private readonly ILogService logService;

        /// <summary>
        /// Initializes a new instance of <see cref="ApplicationRepository"/>.
        /// </summary>
        /// <param name="sqlService"></param>
        /// <param name="logService"></param>
        public ApplicationRepository(ISqlService sqlService, ILogService logService)
        {
            this.sqlService = sqlService;
            this.logService = logService;
        }

        /// <inheritdoc/>
        public async Task<InstallObjectResult> InstallObject(InstallableObject installableObject)
        {
            if (!(installableObject.Content is Application application)) throw new InvalidContentException();
            var result = new InstallObjectResult { Success = true };

            try
            {
                var contentTypeGuid = GetContentTypeId(application.Type);

                var success = await sqlService.OpenConnection(async (c) =>
                    await c.ExecuteAsync(
                        "INSERT INTO ESS_MS_Intern_Page" +
                        " (Guid, IconGuid, ContentType, MenuText, DirectJump, RibbonGroupGuid, RibbonOrderNr, RibbonButtonSize)" +
                        " ON EXISTING UPDATE VALUES" +
                        " (:Id, :IconId, :contentTypeGuid, :Name, :ShortCut, :RibbonGroupId, :RibbonOrderNr, :RibbonButtonSize)",
                        new
                        {
                            application.Id,
                            application.IconId,
                            contentTypeGuid,
                            application.Name,
                            application.Shortcut,
                            application.RibbonGroupId,
                            application.RibbonOrderNr,
                            application.RibbonButtonSize
                        }) > 0
                );

                if (success)
                {
                    if (await SaveConfiguration(application, application.Type))
                    {
                        await logService.WriteAsync($"Installed Application at {installableObject.Target}.", LogLevel.Info);
                    }
                    else
                    {
                        result.Success = false;

                        await logService.WriteAsync(
                            $"Installed Application but failed to install ApplicationConfiguration at {installableObject.Target}.",
                            LogLevel.Error);
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

        /// <summary>
        /// Gets the content type id.
        /// </summary>
        /// <param name="type">The type as string.</param>
        /// <returns>The type id.</returns>
        private static Guid GetContentTypeId(string type)
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
                    throw new Exception(
                        $"Invalid type {type} entered when trying to get ContentType from ESS_MS_Intern_Page_Content.");
            }
        }

        /// <summary>
        /// Saves the configuration data.
        /// </summary>
        /// <param name="application">The application.</param>
        /// <param name="type">the type.</param>
        /// <returns>Returns whether the save was successful.</returns>
        private async Task<bool> SaveConfiguration(Application application, string type)
        {
            var configuration = application.Configuration;
            switch (type)
            {
                case "clr":
                    {
                        if (!(configuration is ClrConfiguration clrConfig))
                            throw new InvalidContentException(
                                "Type was specified to clr, but configuration was not of type ClrConfiguration");

                        var success = await sqlService.OpenConnection(async (c) =>
                        {
                            var affectedRows = await c.ExecuteAsync(
                                "INSERT INTO Application_ClrCall (PageGuid, ClrNamespace, ClrClass, ClrMethod) " +
                                "ON EXISTING UPDATE VALUES (:id, :namespace, :class, :method)",
                                new { application.Id, clrConfig.Namespace, clrConfig.Class, clrConfig.Method });
                            return affectedRows > 0;
                        });
                        return success;
                    }
                case "python":
                    {
                        if (!(configuration is PythonConfiguration pythonConfig))
                            throw new InvalidContentException(
                                "Type was specified to python, but configuration was not of type PythonConfiguration");

                        var success = await sqlService.OpenConnection(async (c) =>
                        {
                            var affectedRows = await c.ExecuteAsync(
                                "INSERT INTO ESS_MS_Intern_Page_Script (PageGuid, ScriptName, ScriptMethod, classname) " +
                                "on existing update values (:id, :path, :method, :class)",
                                new { application.Id, pythonConfig.Path, pythonConfig.Method, pythonConfig.Class });
                            return affectedRows > 0;
                        });
                        return success;
                    }
                case "grid":
                    {
                        if (!(configuration is GridConfiguration gridConfig))
                            throw new InvalidContentException(
                                "Type was specified to grid, but configuration was not of type GridConfiguration");

                        var success = await sqlService.OpenConnection(async (c) =>
                        {
                            var affectedRows = await c.ExecuteAsync(
                                "DELETE FROM ESS_MS_Intern_Page_DataGrid WHERE PageGuid = :id",
                                new { application.Id });

                            affectedRows += await c.ExecuteAsync(
                                "INSERT INTO ESS_MS_Intern_Page_DataGrid " +
                                "(PageGuid, GridName, LoadOnOpen, DataConnectionString, SearchName) " +
                                " ON EXISTING UPDATE VALUES (:id, :grid, :loadOnOpen, :connection, :searchName)",
                                new
                                {
                                    id = application.Id,
                                    gridConfig.Grid,
                                    gridConfig.LoadOnOpen,
                                    gridConfig.Connection,
                                    gridConfig.SearchName
                                });
                            return affectedRows > 0;
                        });
                        return success;
                    }
                case "browser":
                    {
                        if (!(configuration is BrowserConfiguration browserConfig))
                            throw new InvalidContentException(
                                "Type was specified to python, but configuration was not of type PythonConfiguration");

                        var success = await sqlService.OpenConnection(async (c) =>
                        {
                            var affectedRows = await c.ExecuteAsync(
                                "Insert into Browser_Configuration (PageGuid, TabName, StartUrl) " +
                                "on existing update values (:id, :tab, :url) ",
                                new { application.Id, browserConfig.Tab, browserConfig.Url });
                            return affectedRows > 0;
                        });
                        return success;
                    }
                case "grid-structure":
                    {
                        if (!(configuration is GridStructureConfiguration config))
                            throw new InvalidContentException(
                                "Type was specified to grid-structure, but configuration was not of type GridStructureConfiguration");

                        var stackAdded = 0;
                        foreach (var stack in config.Stacks)
                        {
                            stackAdded += await sqlService.OpenConnection(async (c) =>
                                await c.ExecuteAsync(
                                    "INSERT INTO ESS_DCC_Structure_Stack " +
                                    "(Guid, ParentGuid, StackGuid, ShowStackNode, DifGridName, DifDisplayName, OrderNr, DifSearchName) " +
                                    "ON EXISTING UPDATE VALUES " +
                                    "(:Id, :ApplicationId, :StackId, :IsVisible, :Grid, :DisplayName, :OrderId, :SearchName)",
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
                                }));

                            foreach (var register in stack.Registers)
                            {
                                await sqlService.OpenConnection(async (c) =>
                                    await c.ExecuteAsync(
                                        "INSERT INTO ESS_DCC_Structure_Stack_Register " +
                                        "(Guid, ParentGuid, RegisterGuid, DifGridName, DifDisplayName, OrderNr, DifSearchName) " +
                                        "ON EXISTING UPDATE VALUES " +
                                        "(:Id, :StackId, :RegisterId, :Grid, :DisplayName, :OrderId, :SearchName)",
                                    new
                                    {
                                        register.Id,
                                        stack.StackId,
                                        register.RegisterId,
                                        register.Grid,
                                        register.DisplayName,
                                        register.OrderId,
                                        register.SearchName
                                    }));
                            }
                        }
                        return stackAdded == config.Stacks.Count;
                    }
                default:
                    throw new Exception($"Got unknown type for inserting configuration into database. Type: {type}.");
            }
        }

        /// <inheritdoc/>
        public Task<UninstallObjectResult> UninstallObject(InstallableObject installableObject)
        {
            throw new NotImplementedException();
        }
    }
}