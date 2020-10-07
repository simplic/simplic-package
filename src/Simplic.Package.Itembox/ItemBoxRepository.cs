﻿using Dapper;
using Simplic.Sql;
using System;
using System.Linq;
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

        public Task<CheckMigrationResult> CheckMigration(InstallableObject installableObject)
        {
            throw new NotImplementedException();
        }

        // TODO: Output if this fails?
        public async Task<InstallObjectResult> InstallObject(InstallableObject installableObject)
        {
            if (installableObject.Content is DeserializedItemBox itemBox)
            {
                var totalAffectedRows = await sqlService.OpenConnection(async (c) =>
                {
                    return await c.ExecuteAsync("Insert into ESS_MS_Controls_ItemBox (id, name, title, description) on existing update values " +
                                                "(:id, :name, :title, :description); ", new { itemBox.Id, itemBox.Name, itemBox.Title, itemBox.Description });
                });

                // TODO: Smart way of detecting if exists or just add guid? Probably easier
                if (totalAffectedRows == 1)
                {
                    foreach (var profile in itemBox.Profiles)
                    {
                        totalAffectedRows += await sqlService.OpenConnection(async (c) =>
                        {
                            return await c.ExecuteAsync("Insert into ESS_MS_Controls_ItemBox_Profiles (displayname, type, grid, isactive, defaultsearchstring, regex) " +
                                                        "on existing update values (:displayname, :type, :grid, :isactive, :defaultsearchstring, :regex);",
                                                        new { profile.DisplayName, profile.Type, profile.Grid, profile.IsActive, profile.DefaultSearchString, profile.Regex });
                        });
                    }
                }
                return new InstallObjectResult
                {
                    Success = totalAffectedRows == itemBox.Profiles.Count() + 1,
                    LogLevel = LogLevel.Info,
                    Message = $"Succesfully installed ItemBox at {installableObject.Target}"
                };
            }
            throw new InvalidContentException();
        }

        public Task<UninstallObjectResult> UninstallObject(InstallableObject installableObject)
        {
            throw new NotImplementedException();
        }
    }
}