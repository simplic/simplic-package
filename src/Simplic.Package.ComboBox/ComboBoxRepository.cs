using Dapper;
using Simplic.Sql;
using System;
using System.Threading.Tasks;

namespace Simplic.Package.ComboBox
{
    /// <summary>
    /// Repository to write and check combo boxes from the database.
    /// </summary>
    public class ComboBoxRepository : IObjectRepository
    {
        private readonly ISqlService sqlService;
        private readonly ILogService logService;

        /// <summary>
        /// Initialises a new instance of <see cref="ComboBoxRepository"/>
        /// </summary>
        /// <param name="sqlService"></param>
        /// <param name="logService"></param>
        public ComboBoxRepository(ISqlService sqlService, ILogService logService)
        {
            this.sqlService = sqlService;
            this.logService = logService;
        }

        /// <inheritdoc/>
        public async Task<InstallObjectResult> InstallObject(InstallableObject installableObject)
        {
            if (installableObject.Content is ComboBox comboBox)
            {
                var result = new InstallObjectResult { Success = true };

                try
                {
                    var exists = await sqlService.OpenConnection(async (c) =>
                    {
                        var first = await c.QueryFirstOrDefaultAsync("Select * from ESS_MS_Controls_DropDownBox where sqlstatement = :sqlstatement and boxname = :name;",
                                                                     new { comboBox.SqlStatement, comboBox.Name });
                        return first != null;
                    });

                    if (!exists)
                    {
                        var affectedRows = await sqlService.OpenConnection(async (c) =>
                        {
                            return await c.ExecuteAsync("Insert into ESS_MS_Controls_DropDownBox (sqlstatement, boxname, selectcolumn, selectcolumnbackground, fscontrolname) values " +
                                                        "(:sqlstatement, :name, :selectcolumn, :selectcolumnbackground, :fscontrolname)",
                                                        new { comboBox.SqlStatement, comboBox.Name, comboBox.SelectColumn, comboBox.SelectColumnBackground, comboBox.FsControlName });
                        });

                        await logService.WriteAsync($"Installed ComboBox at {installableObject.Target}.", LogLevel.Info);
                    }
                    else
                    {
                        await logService.WriteAsync($"Didnt install ComboBox at {installableObject.Target}, because it already existed.", LogLevel.Info);
                    }
                }
                catch (Exception ex)
                {
                    await logService.WriteAsync($"Failed to install ComboBox at {installableObject.Target}.", LogLevel.Error, ex);
                    result.Success = false;
                }

                return result;
            }
            throw new InvalidContentException();
        }

        /// <inheritdoc/>
        public async Task<UninstallObjectResult> UninstallObject(InstallableObject installableObject)
        {
            if (installableObject.Content is ComboBox comboBox)
            {
                var result = new UninstallObjectResult
                {
                    LogLevel = LogLevel.Info
                };

                try
                {
                    var deletedRows = await sqlService.OpenConnection(async (x) =>
                    {
                        return await x.ExecuteAsync("Delete from ESS_MS_Controls_DropDownBox");
                    });

                    result.Success = true;
                    result.Message = $"Deleted {deletedRows} ComboBoxes.";
                }
                catch (Exception ex)
                {
                    result.Message = $"Failed to delete ComboBox at {installableObject.Target}.";
                    result.LogLevel = LogLevel.Error;
                    result.Exception = ex;
                }

                return result;
            }
            throw new InvalidContentException();
        }
    }
}