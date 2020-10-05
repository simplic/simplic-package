using Dapper;
using Simplic.Sql;
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
            var addedPackageTable = await sqlService.OpenConnection(async (connection) =>
            {
                var updatedRows = await connection.ExecuteAsync("create table if not exists Package ( " +
                                                                "Guid uniqueidentifier primary key, " +
                                                                "PackageName varchar(200) unique not null, " +
                                                                "Major integer not null, " +
                                                                "Minor integer not null, " +
                                                                "Build integer not null, " +
                                                                "Revision integer not null);");
                return updatedRows == 1;
            });

            var addedPackageObjectTable = await sqlService.OpenConnection(async (connection) =>
            {
                var updatedRows = await connection.ExecuteAsync("create table if not exists Package_Object ( " +
                                                                "Guid uniqueidentifier primary key, " +
                                                                "ObjectType varchar(200) not null, " +
                                                                "Target varchar(400) not null, " +
                                                                "PackageGuid uniqueidentifier not null references Package (Guid), " +
                                                                "PackageVersionMajor integer not null, " +
                                                                "PackageVersionMinor integer not null, " +
                                                                "PackageVersionBuild integer not null, " +
                                                                "PackageVersionRevision integer not null);");
                return updatedRows == 1;
            });

            var initializePackageSystemResult = new InitializePackageSystemResult
            {
                LogLevel = LogLevel.Info
            };

            if (addedPackageObjectTable && addedPackageTable)
                initializePackageSystemResult.Message = "Created tables Package and PackageObject";
            else if (addedPackageTable)
                initializePackageSystemResult.Message = "Created table Package, PackageObject existed already";
            else if (addedPackageObjectTable)
                initializePackageSystemResult.Message = "Created table Package_Object, Package existed already";
            else
                initializePackageSystemResult.Message = "Tables Package and PackageObject both existed already";

            return initializePackageSystemResult;
        }
    }
}