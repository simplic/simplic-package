using Dapper;
using Simplic.Sql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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

        /// <summary>
        /// Gets all installed versions of a given package from the database
        /// </summary>
        /// <param name="packageName">The name of the package</param>
        /// <returns>An Enumerable of the installed versions</returns>
        public async Task<IEnumerable<Version>> GetPackageVersions(string packageName)
        {
            var rows = await sqlService.OpenConnection(
                    async (c) => await c.QueryAsync("Select major, minor, build, revision from Package where packagename = :packageName", new { packageName }
                ));
            return rows.Select(row => new Version(row.major, row.minor, row.build, row.revision));
        }

        /// <summary>
        /// Gets the latest installed version of a given package
        /// </summary>
        /// <param name="packageName">The name of the package</param>
        /// <returns>The latest version or null if no version exists</returns>
        public async Task<Version> GetLatestPackageVersion(string packageName)
        {
            var versions = await GetPackageVersions(packageName);
            var versionList = versions.ToList();
            versionList.Sort((x, y) => x.CompareTo(y));

            return versionList.LastOrDefault();
        }

        /// <summary>
        /// Adds the version of a given package to the database
        /// </summary>
        /// <param name="packageName">The name of the package</param>
        /// <param name="version">The version of the package</param>
        /// <param name="guid">The guid of the package</param>
        /// <returns>Whether adding the version worked</returns>
        /// 
        public async Task<bool> AddPackgageVersion(string packageName, Guid guid, Version version)
        {
            var affectedRows = await sqlService.OpenConnection(
                async (c) => await c.ExecuteAsync("Insert into Package (guid, packagename, major, minor, build, revision) " +
                                                    "values (:packageName, :major, :minor, :build, :revision)",
                                                    new
                                                    {
                                                        guid,
                                                        packageName,
                                                        version.Major,
                                                        version.Minor,
                                                        version.Build,
                                                        version.Revision
                                                    }));
            return affectedRows == 1;
        }
    }
}