using Dapper;
using Simplic.Sql;
using System;
using System.Threading.Tasks;
using Telerik.Windows.Data;

namespace Simplic.Package.ItemBox
{
    public class ItemBoxRepository : IObjectRepository
    {
        private readonly ISqlService sqlService;
        private readonly ILogService logService;

        public ItemBoxRepository(ISqlService sqlService, ILogService logService)
        {
            this.sqlService = sqlService;
            this.logService = logService;
        }

        // TODO: Output if this fails?
        public async Task<InstallObjectResult> InstallObject(InstallableObject installableObject)
        {
            if (installableObject.Content is ItemBox itemBox)
            {
                var result = new InstallObjectResult { Success = true };

                try
                {
                    // Get the ident
                    var ident = await sqlService.OpenConnection(async (c) =>
                    {
                        var _ident = await c.QueryFirstOrDefaultAsync<int?>("Select ident from ESS_MS_Controls_ItemBox where name = :name",
                                                            new { itemBox.Name, itemBox.Title, itemBox.Description });

                        if (_ident != null)
                            await logService.WriteAsync($" ItemBox already exists, update: {itemBox.Name}", LogLevel.Debug);
                        else
                        {
                            await logService.WriteAsync($" ItemBox not found, create new ItemBox: {itemBox.Name}", LogLevel.Debug);

                            _ident = await c.QueryFirstAsync<int>("SELECT GET_IDENTITY('ESS_MS_Controls_ItemBox')");
                        }

                        return _ident;
                    });

                    var totalAffectedRows = await sqlService.OpenConnection(async (c) =>
                    {
                        return await c.ExecuteAsync("Insert into ESS_MS_Controls_ItemBox (ident, name, title, description) on existing update values " +
                                                    "(:ident, :name, :title, :description); ", new { ident, itemBox.Name, itemBox.Title, itemBox.Description });
                    });

                    foreach (var profile in itemBox.Profiles)
                    {
                        var statement = GetStatement(profile.Type, profile.Grid);

                        // Attempt to update
                        var updated = await sqlService.OpenConnection(async (c) =>
                        {
                            var rowsAffected = await c.ExecuteAsync("Update ESS_MS_Controls_ItemBox_Profiles " +
                                                                    "set displayname = :displayname," +
                                                                    " defaultsearchstring = :defaultsearchstring," +
                                                                    " isactive = :isactive," +
                                                                    " regex = :regex" +
                                                                    " where selectstatement = :statement and itemboxident = :ident",
                                                                    new
                                                                    {
                                                                        profile.DisplayName,
                                                                        profile.DefaultSearchString,
                                                                        profile.IsActive,
                                                                        profile.Regex,
                                                                        statement,
                                                                        ident
                                                                    });
                            return rowsAffected > 0;
                        });

                        if (!updated)
                        {
                            var inserted = await sqlService.OpenConnection(async (c) =>
                            {
                                var rowsAffected = await c.ExecuteAsync("Insert into ESS_MS_Controls_ItemBox_Profiles (selectstatement, displayname, defaultsearchstring, itemboxident, isactive, regex) " +
                                                                        "values (:statement, :displayname, :defaultsearchstring, :ident, :isactive, :regex)",
                                                                        new { statement, profile.DisplayName, profile.DefaultSearchString, ident, profile.IsActive, profile.Regex });
                                return rowsAffected > 0;
                            });
                        }
                    }

                    result.Success = true;

                    await logService.WriteAsync($"Succesfully installed ItemBox at {installableObject.Target}.", LogLevel.Info);
                }
                catch (Exception ex)
                {
                    await logService.WriteAsync($"Failed to install ItemBox at {installableObject.Target}.", LogLevel.Error, ex);
                    result.Success = false;
                }
                return result;
            }
            throw new InvalidContentException();
        }

        private string GetStatement(string type, string name)
        {
            if (type == "grid" && !name.Contains("grid("))
                return $"grid({name})";

            return name;
        }

        public Task<UninstallObjectResult> UninstallObject(InstallableObject installableObject)
        {
            throw new NotImplementedException();
        }
    }
}