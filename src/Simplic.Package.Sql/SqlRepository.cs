using Simplic.Sql;
using System;
using System.Threading.Tasks;

namespace Simplic.Package.Sql
{
    /// <summary>
    /// Repository to check and and write sql exports to the database.
    /// </summary>
    public class SqlRepository : IObjectRepository
    {
        private readonly ISqlService sqlService;
        private readonly ILogService logService;

        /// <summary>
        /// Initializes a new instance of <see cref="SqlRepository"/>.
        /// </summary>
        /// <param name="sqlService"></param>
        /// <param name="logService"></param>
        public SqlRepository(ISqlService sqlService, ILogService logService)
        {
            this.sqlService = sqlService;
            this.logService = logService;
        }

        /// <inheritdoc/>
        public async Task<InstallObjectResult> InstallObject(InstallableObject installableObject)
        {
            if (installableObject.Content is SqlContent sqlContent)
            {
                var result = new InstallObjectResult { Success = true };

                try
                {
                    await sqlService.OpenConnection(async (c) =>
                    {
                        var command = c.CreateCommand();
                        command.CommandText = sqlContent.Data;

                        command.ExecuteNonQuery();
                    });

                    result.Success = true;
                    await logService.WriteAsync($"Succesfully executed sqlscript: {sqlContent.Data}\n at {installableObject.Target}.", LogLevel.Info);
                }
                catch (Exception ex)
                {
                    await logService.WriteAsync($"Failed to execute sqlscript:{sqlContent.Data}\n at {installableObject.Target}.", LogLevel.Error, ex);

                    result.Success = false;
                }
                return result;
            }
            throw new InvalidContentException("");
        }

        /// <inheritdoc/>
        public Task<UninstallObjectResult> UninstallObject(InstallableObject installableObject)
        {
            throw new NotImplementedException();
        }
    }
}