using Dapper;
using Simplic.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package.Data.DB
{
    public class MigrationRepository : IMigrationRepository
    {
        private readonly ISqlService sqlService;

        public MigrationRepository(ISqlService sqlService)
        {
            this.sqlService = sqlService;
        }

        public async Task<CheckMigrationResult> CheckMigration(InstallableObject installableObject)
        {
            var result = new CheckMigrationResult
            {
                LogLevel = LogLevel.Info
            };

            try
            {
                var row = await sqlService.OpenConnection(async (c) =>
                {
                    return await c.QueryFirstOrDefaultAsync("Select * from Package_Object where guid = :guid", new { installableObject.Guid });
                });

                result.CanMigrate = row == null;
                if (result.CanMigrate)
                    result.Message = $"Object at {installableObject.Target} can be migrated.";
                else
                    result.Message = $"Object at {installableObject.Target} wont be migrated, as it was installed already.";
            }
            catch (Exception ex)
            {
                // result.LogLevel = LogLevel.Error;
                // result.Message = $"Missing package system table Package_Object. Initialize package system first.";
                // result.Exception = ex;
                throw new Exception("Missing package system table Package_Object. Install package system first.", ex);
            }
            return result;
        }

        // TODO: This could return a result object aswell, if information should be logged
        public async Task AddMigration(InstallableObject installableObject)
        {
            await sqlService.OpenConnection(async (c) =>
            {
                await c.ExecuteAsync("Insert into Package_Object (guid, objecttype, target, packageguid, packageversionmajor, packageversionminor, packageversionbuild, packageversionrevision) " +
                                        "values (:guid, :type, :target, :packageguid, :packageversionmajor, :packageversionminor, :packageversionbuild, :packageversionrevision)"
                                        , new
                                        {
                                            guid = installableObject.Guid,
                                            type = "sql",
                                            target = installableObject.Target,
                                            packageguid = installableObject.PackageGuid,
                                            packageversionmajor = installableObject.PackageVersion.Major,
                                            packageversionminor = installableObject.PackageVersion.Minor,
                                            packageversionbuild = installableObject.PackageVersion.Build,
                                            packageversionrevision = installableObject.PackageVersion.Revision
                                        });
            });
        }
    }
}
