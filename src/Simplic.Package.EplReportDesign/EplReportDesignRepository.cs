using Dapper;
using Simplic.Sql;
using System;
using System.Threading.Tasks;

namespace Simplic.Package.EplReportDesign
{
    public class EplReportDesignRepository : IObjectRepository
    {
        private readonly ISqlService sqlService;

        public EplReportDesignRepository(ISqlService sqlService)
        {
            this.sqlService = sqlService;
        }

        public async Task<InstallObjectResult> InstallObject(InstallableObject installableObject)
        {
            if (installableObject.Content is DeserializedEplReportDesign eplReportDesign)
            {
                var result = new InstallObjectResult
                {
                    LogLevel = LogLevel.Info
                };

                try
                {
                    result.Success = await sqlService.OpenConnection(async (c) =>
                    {
                        var affectedRows = await c.ExecuteAsync("Insert into EPL_ReportDesign (id, internname, displayname, reportcontent, printerheadwidth) " +
                                                            "on existing update values (:id, :internalname, :displayname, :reportcontent, :printerheadwidth)",
                                                            new
                                                            {
                                                                eplReportDesign.Id,
                                                                eplReportDesign.InternalName,
                                                                eplReportDesign.DisplayName,
                                                                eplReportDesign.ReportContent,
                                                                eplReportDesign.PrinterHeadWidth
                                                            });
                        return affectedRows > 0;
                    });

                    if (result.Success)
                        result.Message = $"Installed EplReportDesign at {installableObject.Target}.";
                    else
                    {
                        result.LogLevel = LogLevel.Warning;
                        result.Message = $"Failed to install EplReportDesign at {installableObject.Target}.";
                    }
                }
                catch (Exception ex)
                {
                    result.LogLevel = LogLevel.Error;
                    result.Message = $"Failed to install EplReportDesign at {installableObject.Target}.";
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