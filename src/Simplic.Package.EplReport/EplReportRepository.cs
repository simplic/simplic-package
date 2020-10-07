using Dapper;
using Simplic.Sql;
using System;
using System.Threading.Tasks;

namespace Simplic.Package.EplReport
{
    public class EplReportRepository : IObjectRepository
    {
        private readonly ISqlService sqlService;

        public EplReportRepository(ISqlService sqlService)
        {
            this.sqlService = sqlService;
        }

        public Task<CheckMigrationResult> CheckMigration(InstallableObject installableObject)
        {
            throw new NotImplementedException();
        }

        public async Task<InstallObjectResult> InstallObject(InstallableObject installableObject)
        {
            if (installableObject.Content is DeserializedEplReport eplReport)
            {
                var result = new InstallObjectResult
                {
                    LogLevel = LogLevel.Info
                };

                try
                {
                    result.Success = await sqlService.OpenConnection(async (c) =>
                    {
                        var affectedRows = await c.ExecuteAsync("Insert into EPL_Report (id, reportid, internname, printername, iscontextlessprintable) " + // sequenceid, ...
                                                            "update on existing values (id, reportdesignid, internalname, printer, iscontextlessprintable)",
                                                            new
                                                            {
                                                                eplReport.Id,
                                                                eplReport.ReportDesignId,
                                                                eplReport.InternalName,
                                                                eplReport.Printer,
                                                                eplReport.IsContextlessPrintable
                                                            });
                        return affectedRows > 0;
                    });

                    if (result.Success)
                        then also save sequence ?
                        result.Message = $"Installed EplReport at {installableObject.Target}.";
                    else
                    {
                        result.LogLevel = LogLevel.Warning;
                        result.Message = $"Failed to install EplReport at {installableObject.Target}.";
                    }
                }
                catch (Exception ex)
                {
                    result.LogLevel = LogLevel.Error;
                    result.Message = $"Failed to install EplReport at {installableObject.Target}.";
                    result.Exception = ex;
                }
                return result;
            }
            throw new InvalidContentException();
        }

        public Task<UninstallObjectResult> UninstallObject(InstallableObject installableObject)
        {
            throw new NotImplementedException();
        }
    }
}