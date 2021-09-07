using Dapper;
using Simplic.Sql;
using System;
using System.Threading.Tasks;

namespace Simplic.Package.EplReport
{
    /// <summary>
    /// Repository to check and write epl reports to the database.
    /// </summary>
    public class EplReportRepository : IObjectRepository
    {
        private readonly ISqlService sqlService;
        private readonly ILogService logService;

        /// <summary>
        /// Initializes a new instance of <see cref="EplReportRepository"/>.
        /// </summary>
        /// <param name="sqlService"></param>
        /// <param name="logService"></param>
        public EplReportRepository(ISqlService sqlService, ILogService logService)
        {
            this.sqlService = sqlService;
            this.logService = logService;
        }

        /// <inheritdoc/>
        public async Task<InstallObjectResult> InstallObject(InstallableObject installableObject)
        {
            if (installableObject.Content is EplReport eplReport)
            {
                var result = new InstallObjectResult { Success = true };

                try
                {
                    var param = new StatementParam
                    {
                        Id = eplReport.Id,
                        InternalName = eplReport.InternalName,
                        IsContextlessPrintable = eplReport.IsContextlessPrintable,
                        Printer = eplReport.Printer,
                        ReportDesignId = eplReport.ReportDesignId
                    };
                    var statement = "Insert into EPL_Report (id, reportid, internname, printername, iscontextlessprintable) " +
                                    "on existing update values (:Id, :ReportDesignId, :InternalName, :Printer, :IsContextlessPrintable)";


                    if (eplReport.Configuration is SqlConfiguration sqlConfiguration)
                    {
                        statement = "Insert into EPL_Report (id, reportid, internname, printername, iscontextlessprintable, sqldatasourcecode) " +
                                    "on existing update values (:Id, :ReportDesignId, :InternalName, :Printer, :IsContextlessPrintable, :SqlDataSourceCode)";
                        param.SqlDataSourceCode = sqlConfiguration.SqlDataSourceCode;
                    }
                    else if (eplReport.Configuration is SequenceConfiguration sequenceConfiguration)
                    {
                        statement = "Insert into EPL_Report (id, reportid, internname, printername, iscontextlessprintable, sequenceid) " +
                                    "on existing update values (:Id, :ReportDesignId, :InternalName, :Printer, :IsContextlessPrintable, :SequenceId)";
                        param.SequenceId = sequenceConfiguration.SequenceId;
                    }
                    else if (eplReport.Configuration is GridConfiguration gridConfiguration)
                    {
                        // Grid has no additional properties
                    }

                    var execResult = await sqlService.OpenConnection(async (c) =>
                    {
                        var affectedRows = await c.ExecuteAsync(statement, param);
                        return affectedRows > 0;
                    });

                    if (execResult)
                    {
                        await logService.WriteAsync($"Installed EplReport at {installableObject.Target}.", LogLevel.Info);
                    }
                    else
                    {
                        await logService.WriteAsync($"Failed to install EplReport at {installableObject.Target}.", LogLevel.Warning);
                    }
                }
                catch (Exception ex)
                {
                    await logService.WriteAsync($"Failed to install EplReport at {installableObject.Target}.", LogLevel.Error, ex);
                    result.Success = false;
                }
                return result;
            }
            throw new InvalidContentException();
        }

        /// <inheritdoc/>
        public Task<UninstallObjectResult> UninstallObject(InstallableObject installableObject)
        {
            throw new NotImplementedException();
        }
    }
}