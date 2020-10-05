using Simplic.Sql;
using System;
using System.Threading.Tasks;

namespace Simplic.Package.Application
{
    public class ApplicationRepository : IObjectRepository
    {
        private readonly ISqlService sqlService;

        public ApplicationRepository(ISqlService sqlService)
        {
            this.sqlService = sqlService;
        }

        public Task<CheckMigrationResult> CheckMigration(InstallableObject installableObject)
        {
            throw new NotImplementedException();
        }

        public Task<InstallObjectResult> InstallObject(InstallableObject installableObject)
        {
            if (installableObject.Content is DeserializedApplication application)
            {
                var result = new InstallObjectResult
                {
                    LogLevel = LogLevel.Info
                };

                try
                {
                    dont have table
                }
            }
        }
    }
}