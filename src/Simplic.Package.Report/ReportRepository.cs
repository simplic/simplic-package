using AutoMapper;
using Simplic.Package.Report.Model;
using Simplic.Reporting;
using System;
using System.Collections.ObjectModel;
using System.Linq;
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
                    var mapConfig = new MapperConfiguration(cfg => cfg.CreateMap<DeserializedReport, IReportConfiguration>());
                    if (report.Report.Configuration is KeyValueConfiguration configuration)
                    {
                        mapConfig = new MapperConfiguration(cfg =>
                        {
                            cfg.CreateMap<KeyValueParameterItem, KeyValueParameterConfigurationModel>(MemberList.Source)
                                .ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.Name))
                                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(x => x.OrderId));
                            cfg.CreateMap<DeserializedReport, KeyValueConfigurationModel>(MemberList.Source)
                                //.MapDefaults()
                                .ForMember(dest => dest.ReportName, opt => opt.MapFrom(x => x.ReportFile))
                                .ForMember(dest => dest.Type, opt => opt.MapFrom<EnumResolver>())
                                .ForMember(dest => dest.ConnectionString, opt => opt.MapFrom(x => x.Configuration.Connection))
                                .ForMember(dest => dest.PrinterName, opt => opt.MapFrom(x => x.Configuration.Provider))
                                .ForMember(dest => dest.IsListBased, opt => opt.MapFrom(_ => configuration.IsListBased))
                                .ForMember(dest => dest.Parameter, opt => opt.MapFrom(_ => configuration.Parameter));
                        });
                    }
                    else if (report.Report.Configuration is ParameterConfiguration parameterConfiguration)
                    {
                        mapConfig = new MapperConfiguration(cfg => cfg.CreateMap<DeserializedReport, ParameterConfigurationModel>(MemberList.Source)
                                                                .ForMember(dest => dest.ReportName, opt => opt.MapFrom(x => x.ReportFile))
                                                                .ForMember(dest => dest.Type, opt => opt.MapFrom<EnumResolver>())
                                                                .ForMember(dest => dest.ConnectionString, opt => opt.MapFrom(x => x.Configuration.Connection))
                                                                .ForMember(dest => dest.ProviderName, opt => opt.MapFrom(x => x.Configuration.Provider))
                                                                .ForMember(dest => dest.ReportParameter, opt => opt.MapFrom(_ => parameterConfiguration.Parameter))
                                                                );
                    }
                    mapConfig.AssertConfigurationIsValid();
                    var mapper = new Mapper(mapConfig);

                    IReportConfiguration mappedReportConfiguration = null;
                    if (report.Report.Configuration is KeyValueConfiguration)
                        mappedReportConfiguration = mapper.Map<DeserializedReport, KeyValueConfigurationModel>(report.Report);
                    else if (report.Report.Configuration is ParameterConfiguration)
                        mappedReportConfiguration = mapper.Map<DeserializedReport, ParameterConfigurationModel>(report.Report);
                    else
                        throw new NotImplementedException();

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