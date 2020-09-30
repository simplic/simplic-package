using Dapper;
using Simplic.Sql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package.Data.DB
{
    public class PackageTrackingRepository : IPackageTrackingRepository
    {
        private readonly ISqlService sqlService;
        public PackageTrackingRepository(ISqlService sqlService)
        {
            this.sqlService = sqlService;
        }

        public async Task<IEnumerable<Version>> GetPackageVersions(string packageName)
        {
            // TODO: This shouldent be select *
            Func<IDbConnection, Task<IEnumerable<Version>>> func = async conn => await conn.QueryAsync<Version>("Select * from PACKAGETABLE where packagename = :packageName", new { packageName });
            var versions = await sqlService.OpenConnection(func);

            return versions;
        }

        public async Task<Version> GetLatestPackageVersion(string packageName)
        {
            var versions = await GetPackageVersions(packageName);
            var versionList = versions.ToList();
            versionList.Sort((x, y) => x.CompareTo(y));

            return versionList.Last();
        }

        public async Task<int> AddPackgageVersion(string packageName, Version version)
        {
            var success = await sqlService.OpenConnection(
                async (conn) => await conn.ExecuteAsync("Insert into InstalledPackages (package_name, major, minor, build, revision) values (:packageName, :major, :minor, :build, :revision",
                                            new { packageName = packageName, major = version.Major, minor = version.Minor, build = version.Build, revision = version.Revision }
            ));
            return success;
        }
    }
}
