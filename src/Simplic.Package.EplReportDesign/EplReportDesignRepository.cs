using Dapper;
using Simplic.Sql;
using System;
using System.Threading.Tasks;

namespace Simplic.Package.EplReportDesign
{
    public class EplReportDesignRepository : IObjectRepository
    {
        private readonly ISqlService sqlService;
        private readonly ILogService logService;

        public EplReportDesignRepository(ISqlService sqlService, ILogService logService)
        {
            this.sqlService = sqlService;
            this.logService = logService;
        }

        public async Task<InstallObjectResult> InstallObject(InstallableObject installableObject)
        {
            if (installableObject.Content is EplReportDesign eplReportDesign)
            {
                var result = new InstallObjectResult { Success = true };

                try
                {
                    var execResult = await sqlService.OpenConnection(async (c) =>
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

                    if (execResult)
                    {
                        await logService.WriteAsync($"Installed EplReportDesign at {installableObject.Target}.", LogLevel.Info);
                    }
                    else
                    {
                        await logService.WriteAsync($"Failed to install EplReportDesign at {installableObject.Target}.", LogLevel.Warning);
                    }
                }
                catch (Exception ex)
                {
                    await logService.WriteAsync($"Failed to install EplReportDesign at {installableObject.Target}.", LogLevel.Error, ex);

                    result.Success = false;
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