using Dapper;
using Simplic.Sql;
using System;
using System.Threading.Tasks;

namespace Simplic.Package.ComboBox
{
    public class ComboBoxRepository : IObjectRepository
    {
        private readonly ISqlService sqlService;

        public ComboBoxRepository(ISqlService sqlService)
        {
            this.sqlService = sqlService;
        }

        public Task<CheckMigrationResult> CheckMigration(InstallableObject installableObject)
        {
            throw new NotImplementedException();
        }

        public async Task<InstallObjectResult> InstallObject(InstallableObject installableObject)
        {
            if (installableObject.Content is DeserializedComboBox comboBox)
            {
                var result = new InstallObjectResult
                {
                    LogLevel = LogLevel.Info
                };

                try
                {
                    var exists = await sqlService.OpenConnection(async (c) =>
                    {
                        var first = await c.QueryFirstOrDefaultAsync("Select * from ESS_MS_Controls_DropDownBox where sqlstatement = :sqlstatement and boxname = :boxname;",
                                                                     new { comboBox.SqlStatement, comboBox.Name });
                        return first != null;
                    });

                    if (!exists)
                    {
                        var affectedRows = await sqlService.OpenConnection(async (c) =>
                        {
                            return await c.ExecuteAsync("Insert into ESS_MS_Controls_DropDownBox (sqlstatement, boxname, selectcolumn, selectcolumnbackground, fscontrolname) values " +
                                                        "(:sqlstatement, :boxname, :selectcolumn, :selectcolumnbackground, :fscontrolname)",
                                                        new { comboBox.SqlStatement, comboBox.Name, comboBox.SelectColumn, comboBox.SelectColumnBackground, comboBox.FsControlName });
                        });
                        result.Success = true;
                        result.Message = $"Installed ComboBox at {installableObject.Target}.";
                    }
                    else
                    {
                        result.Success = true;
                        result.Message = $"Didnt install ComboBox at {installableObject.Target}, because it already existed.";
                    }
                }
                catch (Exception ex)
                {
                    result.Message = $"Failed to install ComboBox at {installableObject.Target}.";
                    result.LogLevel = LogLevel.Error;
                    result.Exception = ex;
                }

                return result;
            }
            throw new InvalidContentException();
        }

        public async Task<UninstallObjectResult> UninstallObject(InstallableObject installableObject)
        {
            if (installableObject.Content is DeserializedComboBox comboBox)
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