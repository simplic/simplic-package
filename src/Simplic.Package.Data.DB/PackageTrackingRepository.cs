using Dapper;
using Simplic.Sql;
using System;
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
        /// Gets the latest installed version of a given package
        /// </summary>
        /// <param name="guid">The guid of the package</param>
        /// <returns>The latest version or null if no version exists</returns>
        public async Task<Version> GetPackageVersion(Guid guid)
        {
            var version = await sqlService.OpenConnection(async (c) =>
            {
                var row = await c.QueryFirstOrDefaultAsync("Select major, minor, build, revision from Package where guid = :guid", new { guid });

                if (row != null)
                    return new Version(row.major, row.minor, row.build, row.revision);
                return null;
            });

            return version;
        }

        /// <summary>
        /// Gets the latest installed version of a given package
        /// </summary>
        /// <param name="packageName">The guid of the package</param>
        /// <returns>The latest version or null if no version exists</returns>
        public async Task<Version> GetPackageVersion(string packageName)
        {
            var version = await sqlService.OpenConnection(async (c) =>
            {
                var row = await c.QueryFirstOrDefaultAsync("Select major, minor, build, revision from Package where guid = :packageName", new { packageName });

                if (row != null)
                    return new Version(row.major, row.minor, row.build, row.revision);
                return null;
            });

            return version;
        }

        /// <summary>
        /// Adds the version of a given package to the database
        /// </summary>
        /// <param name="package">The package whose version to add</param>
        /// <returns>Whether adding the version worked</returns>
        public async Task<bool> AddPackgageVersion(Package package)
        {
            return await AddPackgageVersion(package.Name, package.Guid, package.Version);
        }

        /// <summary>
        /// Adds the version of a given package to the database
        /// </summary>
        /// <param name="packageName">The name of the package</param>
        /// <param name="version">The version of the package</param>
        /// <param name="guid">The guid of the package</param>
        /// <returns>Whether adding the version worked</returns>
        public async Task<bool> AddPackgageVersion(string packageName, Guid guid, Version version)
        {
            var affectedRows = await sqlService.OpenConnection(
                async (c) => await c.ExecuteAsync("Insert into Package (guid, packagename, major, minor, build, revision) " +
                                                    "on existing update values (:packageName, :major, :minor, :build, :revision)",
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