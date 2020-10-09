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

                try
                {
                    var row = await c.QueryFirstOrDefaultAsync("Select * from Package_Object where guid = :guid", new { installableObject.Guid });

                    if (row != null)
                    {
                        result.CanMigrate = false;
                        result.Message = $"The sql script at {installableObject.Target} wont be migrated as it was already executed.";
                        result.LogLevel = LogLevel.Info;
                    }
                    else
                    {
                        result.CanMigrate = true;
                        result.Message = $"The sql script at {installableObject.Target} will be migrated";
                        result.LogLevel = LogLevel.Info;
                    }
                    return result;
                }
                catch (Exception ex)
                {
                    throw new Exception("Missing package system table Package_Object. Install package system first.", ex);
                }
            });

            return checkMigrationResult;
        }

        public async Task<InstallObjectResult> InstallObject(InstallableObject installableObject)
        {
            if (installableObject.Content is SqlContent sqlContent)
            {
                var installObjectResult = await sqlService.OpenConnection<Task<InstallObjectResult>>(async (c) =>
                {
                    var result = new InstallObjectResult { Success = true };
                    try
                    {
                        var command = c.CreateCommand();
                        command.CommandText = sqlContent.Data;

                        command.ExecuteNonQuery();

                        result.Message = $"Succesfully executed sqlscript: {sqlContent.Data}\n at {installableObject.Target}.";
                        result.LogLevel = LogLevel.Info;
                    }
                    catch (Exception ex)
                    {
                        result.Success = false;
                        result.Exception = ex;
                        result.Message = $"Failed to execute sqlscript:{sqlContent.Data}\n at {installableObject.Target}.";
                        result.LogLevel = LogLevel.Error;
                    }

                    if (installableObject.Mode == InstallMode.Migrate && result.Success)
                    {
                        try
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
                            result.Message = $"Succesfully executed sql script: {sqlContent.Data}\n and added to Package_Object table at {installableObject.Target}.";
                            result.LogLevel = LogLevel.Info;
                        }
                        catch (Exception ex)
                        {
                            result.Success = false;
                            result.Exception = ex;
                            result.Message = $"Executed sql script: {sqlContent.Data}\n but failed to add it to Package_Object table at {installableObject.Target}.";
                            result.LogLevel = LogLevel.Error;
                        }
                    }

                    return result;
                });

                return installObjectResult;
            }
            throw new InvalidContentException("");
        }

        public Task<UninstallObjectResult> UninstallObject(InstallableObject installableObject)
        {
            throw new NotImplementedException();
        }
    }
}