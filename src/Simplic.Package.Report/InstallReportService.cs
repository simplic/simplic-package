using AutoMapper;
using Simplic.Reporting;
using System;
using System.Threading.Tasks;
using Unity;

namespace Simplic.Package.Report
{
    public class InstallReportService : IInstallObjectService
    {
        public static IMappingExpression<DeserializedReport, T> MapDefaults<T>(IMappingExpression<DeserializedReport, T> map) where T : class, IReportConfiguration
        {
            map.ForMember(dest => dest.ReportName, opt => opt.MapFrom(x => x.ReportFile));
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

                        cfg.CreateMap<DeserializedReport, KeyValueConfigurationModel>(MemberList.Source)
                            .ForMember(dest => dest.ConnectionString, opt => opt.MapFrom(x => x.Configuration.Connection))
                            .ForMember(dest => dest.PrinterName, opt => opt.MapFrom(x => x.Configuration.Provider))
                            .ForMember(dest => dest.IsListBased, opt => opt.MapFrom(x => ((KeyValueConfiguration)x.Configuration).IsListBased))
                            .ForMember(dest => dest.Parameter, opt => opt.MapFrom(x => ((KeyValueConfiguration)x.Configuration).Parameter))
                            .ForMember(dest => dest.ReportName, opt => opt.MapFrom(x => x.ReportFile))
                            .ForMember(dest => dest.Type, opt => opt.MapFrom<EnumResolver>())
                            .ForSourceMember(x => x.Configuration, opt => opt.DoNotValidate());

                        cfg.CreateMap<DeserializedReport, ParameterConfigurationModel>(MemberList.Source)
                            .ForMember(dest => dest.ConnectionString, opt => opt.MapFrom(x => x.Configuration.Connection))
                            .ForMember(dest => dest.ProviderName, opt => opt.MapFrom(x => x.Configuration.Provider))
                            .ForMember(dest => dest.ReportParameter, opt => opt.MapFrom(x => ((ParameterConfiguration)x.Configuration).Parameter))
                            .ForMember(dest => dest.ReportName, opt => opt.MapFrom(x => x.ReportFile))
                            .ForMember(dest => dest.Type, opt => opt.MapFrom<EnumResolver>())
                            .ForSourceMember(x => x.Configuration, opt => opt.DoNotValidate());
                    });

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
                    ReportManager.Singleton.SaveReportFile(report.Report.Name, report.ReportData); // TODO: Simplic.Package.InvalidObjectException: Failed to install Report at report/report_sample1.json. ---> System.Data.Entity.Infrastructure.DbUpdateException: An error occurred while updating the entries. See the inner exception for details. ---> System.Data.Entity.Core.UpdateException: An error occurred while updating the entries. See the inner exception for details. ---> iAnywhere.Data.SQLAnywhere.SAException: Spalte 'UserName' in Tabelle 'Repository_FileHistory' kann nicht NULL sein

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