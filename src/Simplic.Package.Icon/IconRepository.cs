using Dapper;
using Simplic.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package.Icon
{
    public class IconRepository : IObjectRepository
    {
        private readonly ISqlService sqlService;

        public IconRepository(ISqlService sqlService)
        {
            this.sqlService = sqlService;
        }

        public async Task<Guid> GetGuidByName(string iconName)
        {
            return await sqlService.OpenConnection(async (c) =>
            {
                return await c.QueryFirstOrDefault("Select Guid from Icon where Name = :iconName", new { iconName });
            });
        }

        public Task<InstallObjectResult> InstallObject(InstallableObject installableObject)
        {
            throw new NotImplementedException();
        }

        public Task<UninstallObjectResult> UninstallObject(InstallableObject installableObject)
        {
            throw new NotImplementedException();
        }
    }
}
