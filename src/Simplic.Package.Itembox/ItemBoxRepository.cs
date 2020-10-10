using Dapper;
using Simplic.Sql;
using System;
using System.Threading.Tasks;

namespace Simplic.Package.ItemBox
{
    public class ItemBoxRepository : IObjectRepository
    {
        private readonly ISqlService sqlService;

        public ItemBoxRepository(ISqlService sqlService)
        {
            this.sqlService = sqlService;
        }

        // TODO: Output if this fails?
        public async Task<InstallObjectResult> InstallObject(InstallableObject installableObject)
        {
            if (installableObject.Content is DeserializedItemBox itemBox)
            {
                var result = new InstallObjectResult
                {
                    LogLevel = LogLevel.Info
                };

                try
                {
                    var exists = await sqlService.OpenConnection(async (c) =>
                    {
                        var query = await c.QueryFirstOrDefaultAsync("Select * from ESS_MS_Controls_ItemBox where name = :name, title = :title, description = :description",
                                                                        new { itemBox.Name, itemBox.Title, itemBox.Description });
                        return query != null;
                    });

                    if (!exists)
                    {
                        var totalAffectedRows = await sqlService.OpenConnection(async (c) =>
                        {
                            return await c.ExecuteAsync("Insert into ESS_MS_Controls_ItemBox (name, title, description) values " +
                                                        "(:name, :title, :description); ", new { itemBox.Name, itemBox.Title, itemBox.Description });
                        });
                    }

                    // Get the ident
                    var ident = await sqlService.OpenConnection(async (c) =>
                    {
                        return await c.QueryFirstAsync<int>("Select ident from ESS_MS_Controls_ItemBox where name = :name, title = :title, description = :description order by ident desc",
                                                            new { itemBox.Name, itemBox.Title, itemBox.Description });
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
                                var rowsAffected = await c.ExecuteAsync("Insert into ESS_MS_Controls_ItemBox_Profiles " +
                                                                        "(selectstatement, displayname, defaultsearchstring, itemboxident, isactive, regex) " +
                                                                        " values (:statement, :displayname, :defaultsearchstring, :ident, :isactive, :regex)" +
                                                                        new
                                                                        {
                                                                            statement,
                                                                            profile.DisplayName,
                                                                            profile.DefaultSearchString,
                                                                            ident,
                                                                            profile.IsActive,
                                                                            profile.Regex
                                                                        });
                                return rowsAffected > 0;
                            });
                        }
                    }

                    result.Success = true;
                    result.Message = $"Succesfully installed ItemBox at {installableObject.Target}.";
                }
                catch (Exception ex)
                {
                    result.Message = $"Failed to install ItemBox at {installableObject.Target}.";
                    result.LogLevel = LogLevel.Error;
                    result.Exception = ex;
                }
                return result;
            }
            throw new InvalidContentException();
        }

        private string GetStatement(string type, string name)
        {
            if (type == "grid")
                return $"grid({name})";
            return "";
        }

        public Task<UninstallObjectResult> UninstallObject(InstallableObject installableObject)
        {
            throw new NotImplementedException();
        }
    }
}