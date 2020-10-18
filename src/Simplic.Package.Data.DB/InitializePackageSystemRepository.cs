using Dapper;
using Simplic.Sql;
using System.Data;
using System.Printing;
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
            var packageTableExists = await TableExists("Package");
            var packageObjectTableExists = await TableExists("Package_Object");

            if (!packageTableExists)
            {
                await sqlService.OpenConnection(async (connection) =>
                {
                    await connection.ExecuteAsync("create table Package ( " +
                                                    "Guid uniqueidentifier primary key, " +
                                                    "PackageName varchar(200) unique not null, " +
                                                    "Major integer not null, " +
                                                    "Minor integer not null, " +
                                                    "Build integer not null, " +
                                                    "Revision integer not null);");
                });
            }

            if (!packageObjectTableExists)
            {
                await sqlService.OpenConnection(async (connection) =>
                {
                    await connection.ExecuteAsync("create table if not exists Package_Object ( " +
                                                    "Guid uniqueidentifier primary key, " +
                                                    "ObjectType varchar(200) not null, " +
                                                    "Target varchar(400) not null, " +
                                                    "PackageGuid uniqueidentifier not null references Package (Guid), " +
                                                    "PackageVersionMajor integer not null, " +
                                                    "PackageVersionMinor integer not null, " +
                                                    "PackageVersionBuild integer not null, " +
                                                    "PackageVersionRevision integer not null);");
                });
            }

            var initializePackageSystemResult = new InitializePackageSystemResult
            {
                LogLevel = LogLevel.Info
            };

            if (!packageTableExists && !packageObjectTableExists)
                initializePackageSystemResult.Message = "Created tables Package and PackageObject";
            else if (!packageTableExists)
                initializePackageSystemResult.Message = "Created table Package, PackageObject existed already";
            else if (!packageObjectTableExists)
                initializePackageSystemResult.Message = "Created table Package_Object, Package existed already";
            else
                initializePackageSystemResult.Message = "Tables Package and PackageObject both existed already";

            return initializePackageSystemResult;
        }

        private async Task<bool> TableExists(string tableName)
        {
            return await sqlService.OpenConnection(async (c) =>
            {
                var result = await c.QueryFirstOrDefaultAsync("Select * from sys.systable where table_name = :tableName", new { tableName });
                return result != null;
            });
        }
    }
}