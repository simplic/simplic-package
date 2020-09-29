using Dapper;
using Simplic.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package.Data.DB
{
    public class SqlRepository : IObjectRepository
    {
        private readonly ISqlService sqlService;
        private readonly ILogService logService;

        public SqlRepository(ISqlService sqlService, ILogService logService)
        {
            this.sqlService = sqlService;
            this.logService = logService;
        }

        public Task<CheckMigrationResult> CheckMigration(InstallableObject installableObject)
        {
            throw new NotImplementedException();
        }

        public async Task InstallObject(InstallableObject installableObject)
        {
            if (installableObject.Content is SqlContent sqlContent)
            {
                await sqlService.OpenConnection(async (c) =>
                {
                    await c.ExecuteAsync(sqlContent.Data);

                    await logService.WriteAsync(sqlContent.Data, LogLevel.Debug);
                });
            }
        }
    }
}
