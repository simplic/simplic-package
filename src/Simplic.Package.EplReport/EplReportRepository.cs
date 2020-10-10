using Dapper;
using Simplic.Framework.DBUI.Sql;
using Simplic.Sql;
using System;
using System.Reflection.Emit;
using System.Security.Authentication.ExtendedProtection;
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
                    var param = new StatementHelper
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
                        throw new NotImplementedException();
                    }

                    result.Success = await sqlService.OpenConnection(async (c) =>
                    {
                        var affectedRows = await c.ExecuteAsync(statement, param);
                        return affectedRows > 0;
                    });

                    if (result.Success)
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