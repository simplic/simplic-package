using Simplic.Reporting;
using System;
using System.Threading.Tasks;

namespace Simplic.Package.Report
{
    public class ReportRepository : IObjectRepository
    {
        public Task<CheckMigrationResult> CheckMigration(InstallableObject installableObject)
        {
            throw new NotImplementedException();
        }

        public async Task<InstallObjectResult> InstallObject(InstallableObject installableObject)
        {
            if (installableObject.Content is FullReport report)
            {
                var result = new InstallObjectResult
                {
                    LogLevel = LogLevel.Info
                };

                try
                {
                    ReportManager.Singleton.SaveConfiguration(report.Report);
                    ReportManager.Singleton.SaveReportFile(report.Report.Name, report.ReportData);

                    result.Message = $"Installed Report at {installableObject.Target}.";
                }
                catch (Exception ex)
                {
                    result.Message = $"Failed to install Report at {installableObject.Target}.";
                    result.LogLevel = LogLevel.Error;
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