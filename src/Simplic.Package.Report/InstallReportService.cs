using AutoMapper;
using Simplic.Reporting;
using System;
using System.Threading.Tasks;
using Unity;

namespace Simplic.Package.Report
{
    public class InstallReportService : IInstallObjectService
    {
        public static IMappingExpression<Report, T> MapDefaults<T>(IMappingExpression<Report, T> map) where T : class, IReportConfiguration
        {
            map.ForMember(dest => dest.PrinterName, opt => opt.MapFrom(x => x.PrinterName));
            map.ForMember(dest => dest.Type, opt => opt.MapFrom<EnumResolver>());
            map.ForSourceMember(x => x.Configuration, opt => opt.DoNotValidate());

            return map;
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
                    // TODO: Implement for sqlconfiguration
                    var mapConfig = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<KeyValueParameterItem, KeyValueParameterConfigurationModel>(MemberList.Source)
                            .ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.Name))
                            .ForMember(dest => dest.OrderId, opt => opt.MapFrom(x => x.OrderId));

                        cfg.CreateMap<Report, KeyValueConfigurationModel>(MemberList.Source)
                            .ForMember(dest => dest.ConnectionString, opt => opt.MapFrom(x => x.Configuration.Connection))
                            .ForMember(dest => dest.PrinterName, opt => opt.MapFrom(x => x.Configuration.Provider))
                            .ForMember(dest => dest.IsListBased, opt => opt.MapFrom(x => ((KeyValueConfiguration)x.Configuration).IsListBased))
                            .ForMember(dest => dest.Parameter, opt => opt.MapFrom(x => ((KeyValueConfiguration)x.Configuration).Parameter))
                            .ForMember(dest => dest.PrinterName, opt => opt.MapFrom(x => x.PrinterName))
                            .ForMember(dest => dest.Type, opt => opt.MapFrom<EnumResolver>())
                            .ForSourceMember(x => x.Configuration, opt => opt.DoNotValidate());

                        cfg.CreateMap<Report, ParameterConfigurationModel>(MemberList.Source)
                            .ForMember(dest => dest.ConnectionString, opt => opt.MapFrom(x => x.Configuration.Connection))
                            .ForMember(dest => dest.ProviderName, opt => opt.MapFrom(x => x.Configuration.Provider))
                            .ForMember(dest => dest.ReportParameter, opt => opt.MapFrom(x => ((ParameterConfiguration)x.Configuration).Parameter))
                            .ForMember(dest => dest.PrinterName, opt => opt.MapFrom(x => x.PrinterName))
                            .ForMember(dest => dest.Type, opt => opt.MapFrom<EnumResolver>())
                            .ForSourceMember(x => x.Configuration, opt => opt.DoNotValidate());
                    });

                    mapConfig.AssertConfigurationIsValid();
                    var mapper = new Mapper(mapConfig);

                    IReportConfiguration mappedReportConfiguration = null;
                    if (report.Report.Configuration is KeyValueConfiguration)
                        mappedReportConfiguration = mapper.Map<Report, KeyValueConfigurationModel>(report.Report);
                    else if (report.Report.Configuration is ParameterConfiguration)
                        mappedReportConfiguration = mapper.Map<Report, ParameterConfigurationModel>(report.Report);
                    else
                        throw new NotImplementedException();

                    ReportManager.Singleton.SaveConfiguration(mappedReportConfiguration);
                    ReportManager.Singleton.SaveReportFile(mappedReportConfiguration.ReportName, report.ReportData);

                    result.Success = true;
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

        public Task OverwriteObject(InstallableObject installableObject)
        {
            throw new NotImplementedException();
        }

        public Task<UninstallObjectResult> UninstallObject(InstallableObject installableObject)
        {
            throw new NotImplementedException();
        }
    }
}