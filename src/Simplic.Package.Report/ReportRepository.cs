using AutoMapper;
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
                    // TODO: Automapper
                    if (report.Report.Configuration is KeyValueConfiguration configuration)
                    {
                        var parameterMap = new MapperConfiguration(cfg => cfg.CreateMap<KeyValueParameterItem, KeyValueParameterConfigurationModel>(MemberList.Source));
)
                        var mappedParameters = new ObservableCollection<KeyValueParameterConfigurationModel>();
                        foreach (var parameter in configuration.Parameter)
                            mappedParameters.Add(new Mapper(parameterMap).Map<KeyValueParameterConfigurationModel>(parameter));

                        var mapConfig = new MapperConfiguration(cfg => cfg.CreateMap<DeserializedReport, KeyValueConfigurationModel>(MemberList.Source)
                                                                .ForMember(dest => dest.ReportName, opt => opt.MapFrom(x => x.ReportFile))
                                                                .ForMember(dest => dest.Type, opt => opt.MapFrom<EnumResolver>())
                                                                .ForMember(dest => dest.IsListBased, opt => opt.MapFrom(x => configuration.IsListBased))
                                                                .ForMember(dest => dest.ConnectionString, opt => opt.MapFrom(x => configuration.Connection))
                                                                .ForMember(dest => dest.ProviderName, opt => opt.MapFrom(_ => configuration.Provider))
                                                                .ForMember(dest => dest.Parameter, opt => opt.MapFrom(_ => mappedParameters))
                                                                );
                    }
                    var mapConfig = new MapperConfiguration(cfg => cfg.CreateMap<DeserializedReport, Simplic.Reporting.IReportConfiguration>(MemberList.Source)
                                                            .ForMember(dest => dest.ReportName, opt => opt.MapFrom(x => x.ReportFile))
                                                            .ForMember(dest => dest.Type, opt => opt.MapFrom<EnumResolver>())
                                                            .ForSourceMember(src => src.Configuration, opt => opt.DoNotValidate())
                                                            );
                                                            
                                                            
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