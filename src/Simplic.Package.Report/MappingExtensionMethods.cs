using AutoMapper;
using Simplic.Reporting;

namespace Simplic.Package.Report
{
    public static class MappingExtensionMethod
    {
        public static IMappingExpression<T, IReportConfiguration> MapDefaults<T>(this IMappingExpression<T, IReportConfiguration> map) where T : DeserializedReport
        {
            map.ForMember(dest => dest.ReportName, opt => opt.MapFrom(x => x.ReportFile));
            map.ForMember(dest => dest.Type, opt => opt.MapFrom<EnumResolver>());
            map.ForMember(dest => dest.PrinterName, opt => opt.MapFrom(x => x.Configuration.Provider));

            return map;
        }
    }
}
