using Dapper;
using Simplic.Sql;
using System;
using System.Threading.Tasks;

namespace Simplic.Package.Sql
{
    public class SqlRepository : IObjectRepository
    {
        private readonly ISqlService sqlService;

        public SqlRepository(ISqlService sqlService)
        {
            this.sqlService = sqlService;
        }

        public async Task<CheckMigrationResult> CheckMigration(InstallableObject installableObject)
        {
            var checkMigrationResult = await sqlService.OpenConnection<Task<CheckMigrationResult>>(async (c) =>
            {
                var result = new CheckMigrationResult();

                // TODO: try catch to tell user that he must initialize first
                var row = await c.QueryFirstOrDefaultAsync("Select * from Package_Object where guid = :guid", new { installableObject.Guid });

                if (row != null)
                {
                    result.CanMigrate = false;
                    result.Message = $"The sql script @ {installableObject.Target} wont be migrated as it was already executed.";
                    result.LogLevel = LogLevel.Info;
                }
                else
                {
                    result.CanMigrate = true;
                    result.Message = $"The sql script @ {installableObject.Target} will be migrated";
                    result.LogLevel = LogLevel.Info;
                }
                return result;
            });

            return checkMigrationResult;
        }

        public async Task<InstallObjectResult> InstallObject(InstallableObject installableObject)
        {
            var sqlContent = installableObject.Content as SqlContent;

            var installObjectResult = await sqlService.OpenConnection<Task<InstallObjectResult>>(async (c) =>
            {
                var result = new InstallObjectResult { Success = true };
                try
                {
                    await c.ExecuteAsync(sqlContent.Data);
                    result.Message = $"Succesfully executed {installableObject.Content}!";
                    result.LogLevel = LogLevel.Info;
                }
                catch (Exception ex)
                {
                    result.Success = false;
                    result.Exception = ex;
                    result.Message = $"Failed to execute {installableObject.Content}!";
                    result.LogLevel = LogLevel.Error;
                }

                if (installableObject.Mode == InstallMode.Migrate && result.Success)
                {
                    try
                    {
                        await c.ExecuteAsync("Insert into Package_Object (guid, objecttype, target, package, packageversionmaajor, packageversionminor, packageversionbuild, packageversionrevision ) " +
                                            "values (:guid, 'sql', :target, :package, :packageversionmajor, :packageversionminor, :packageversionbuild, :packageversionrevision)"
                                            , new
                                            {
                                                installableObject.Guid,
                                                installableObject.Target,
                                                installableObject.PackageName,
                                                installableObject.PackageVersion.Major,
                                                installableObject.PackageVersion.Minor,
                                                installableObject.PackageVersion.Build,
                                                installableObject.PackageVersion.Revision
                                            });
                        result.Message = $"Succesfully executed and added to Package_Object table! Target:{installableObject.Target}, content:{installableObject.Content}!";
                        result.LogLevel = LogLevel.Info;
                    }
                    catch (Exception ex)
                    {
                        result.Success = false;
                        result.Exception = ex;
                        result.Message = $"Executed but failed to add to Package_Object table! Target:{installableObject.Target}, content:{installableObject.Content}!";
                        result.LogLevel = LogLevel.Error;
                    }
                }

                return result;
            });
            return installObjectResult;
        }
    }
}
