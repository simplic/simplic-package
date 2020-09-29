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
    public class VersionRepository : IVersionRepository
    {
        private readonly ISqlService sqlService;
        public VersionRepository(ISqlService sqlService)
        {
            this.sqlService = sqlService;
        }

        public async Task<IEnumerable<Version>>GetVersionsAsync(string packageName)
        {
            // TODO: This shouldent be select *
            Func<IDbConnection, Task<IEnumerable<Version>>> func = async conn => await conn.QueryAsync<Version>("Select * from PACKAGETABLE where packagename = :packageName", new { packageName });
            var versions = await sqlService.OpenConnection(func);

            return versions;
        }

        public async Task<Version> GetLatestVersionAsync(string packageName)
        {
            var versions = await GetVersionsAsync(packageName);
            var versionList = versions.ToList();
            versionList.Sort((x, y) => x.CompareTo(y));

            return versionList.Last();
        }
    }
}
