using Dapper;
using Simplic.Sql;
using System;
using System.Threading.Tasks;

namespace Simplic.Package.Data.DB
{
    public class InitializePackageSystemRepository : IInitializePackageSystemRepository
    {
        private readonly ISqlService sqlService;

        public InitializePackageSystemRepository(ISqlService sqlService)
        {
            this.sqlService = sqlService;
        }

        public async Task<InitializePackageSystemResult> Intialize()
        {
            var initializePackageSystemResult = new InitializePackageSystemResult
            {
                LogLevel = LogLevel.Info
            };

            try
            {
                await sqlService.OpenConnection(async (connection) =>
                {
                    await connection.ExecuteAsync("create table if not exists Package ( " +
                                                    "Guid uniqueidentifier primary key, " +
                                                    "PackageName varchar(200) not null, " +
                                                    "Major integer not null, " +
                                                    "Minor integer not null, " +
                                                    "Build integer not null, " +
                                                    "Revision integer not null);");

                });
                initializePackageSystemResult.CreatedTablePackage = true;
            }
            catch (Exception ex)
            {
                initializePackageSystemResult.Exception = ex;
                initializePackageSystemResult.LogLevel = LogLevel.Error;
            }

            try
            {

                await sqlService.OpenConnection(async (connection) =>
                {
                    // TODO: Switch to package-guid and add foreign key
                    await connection.ExecuteAsync("create table if not exists Package_Object ( " +
                                                    "Guid uniqueidentifier primary key, " +
                                                    "ObjectType varchar(200) not null, " +
                                                    "Target varchar(400) not null, " +
                                                    "Package varchar(200) not null, " +
                                                    "PackageVersionMajor integer not null, " +
                                                    "PackageVersionMinor integer not null, " +
                                                    "PackageVersionBuild integer not null, " +
                                                    "PackageVersionRevision integer not null);");
                });
                initializePackageSystemResult.CreatedTablePackageObject = true;
            }
            catch (Exception ex)
            {
                initializePackageSystemResult.Exception = ex;
                initializePackageSystemResult.LogLevel = LogLevel.Error;
            }

            if (initializePackageSystemResult.CreatedTablePackage && initializePackageSystemResult.CreatedTablePackageObject)
                initializePackageSystemResult.LogMessage = "Created tables Package and PackageObject";
            else if (initializePackageSystemResult.CreatedTablePackage)
                initializePackageSystemResult.LogMessage = "Created table Package, failed to create PackageObject";
            else if (initializePackageSystemResult.CreatedTablePackageObject)
                initializePackageSystemResult.LogMessage = "Created table Package_Object, failed to create Package";
            else
                initializePackageSystemResult.LogMessage = "Failed to create tables Package and PackageObject";

            return initializePackageSystemResult;
        }
    }
}
