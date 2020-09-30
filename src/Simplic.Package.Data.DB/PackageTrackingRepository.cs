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
    // TODO: change to correct table names!
    public class PackageTrackingRepository : IPackageTrackingRepository
    {
        private readonly ISqlService sqlService;
        public PackageTrackingRepository(ISqlService sqlService)
        {
            this.sqlService = sqlService;
        }

        public async Task<IEnumerable<Version>> GetPackageVersions(string packageName)
        {
            var rows = await sqlService.OpenConnection(
                    async (c) => await c.QueryAsync("Select major, minor, build, revision from Package where package_name = :packageName", new { packageName }
                ));
            return rows.Select(row => new Version(row.major, row.minor, row.build, row.revision));
        }

        public async Task<Version> GetLatestPackageVersion(string packageName)
        {
            var versions = await GetPackageVersions(packageName);
            var versionList = versions.ToList();
            versionList.Sort((x, y) => x.CompareTo(y));

            return versionList.LastOrDefault();
        }

        public async Task<bool> AddPackgageVersion(string packageName, Version version)
        {
            var affectedRows = await sqlService.OpenConnection(
                async (c) => await c.ExecuteAsync("Insert into Package (package_name, major, minor, build, revision) values (:packageName, :major, :minor, :build, :revision)",
                                            new { packageName = packageName, major = version.Major, minor = version.Minor, build = version.Build, revision = version.Revision }
            ));
            return affectedRows == 1;
        }
    }
}
