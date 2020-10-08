using AutoMapper;
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
                    var mapConfig = new MapperConfiguration(cfg => cfg.CreateMap<DeserializedReport, Simplic.Reporting.IReportConfiguration>()
                                                            .ForMember(x => x.Type, opt => opt.MapFrom<EnumResolver>()));
                    mapConfig.AssertConfigurationIsValid();
                    var mapper = new Mapper(mapConfig);

                    var mappedReportConfiguration = mapper.Map<DeserializedReport, Simplic.Reporting.IReportConfiguration>(report.Report);
                    ReportManager.Singleton.SaveConfiguration(mappedReportConfiguration);
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

        private static Simplic.Reporting.ReportType ResolveEnum(string type)
        {
            return ReportType.None;
        }

        public Task<UninstallObjectResult> UninstallObject(InstallableObject installableObject)
        {
            throw new NotImplementedException();
        }
    }
}