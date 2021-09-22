using Dapper;
using Simplic.Sql;
using System;
using System.Threading.Tasks;

namespace Simplic.Package.EplReportDesign
{
    /// <summary>
    /// Repository to check and write objects to a database.
    /// </summary>
    public class EplReportDesignRepository : IObjectRepository
    {
        private readonly ISqlService sqlService;
        private readonly ILogService logService;

        /// <summary>
        /// Initializes a new instance of <see cref="EplReportDesignRepository"/>.
        /// </summary>
        /// <param name="sqlService"></param>
        /// <param name="logService"></param>
        public EplReportDesignRepository(ISqlService sqlService, ILogService logService)
        {
            this.sqlService = sqlService;
            this.logService = logService;
        }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public Task<UninstallObjectResult> UninstallObject(InstallableObject installableObject)
        {
            throw new NotImplementedException();
        }
    }
}