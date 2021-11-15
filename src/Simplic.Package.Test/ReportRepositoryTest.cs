using AutoMapper;
using Simplic.Package.Report;
using Simplic.Reporting;
using Xunit;

namespace Simplic.Package.Test
{
    public class ReportRepositoryTest
    {
        [Theory]
        [InlineData("key-value", ReportType.KeyValueReport)]
        [InlineData("sql", ReportType.SqlReport)]
        [InlineData("parameter", ReportType.ParameterReport)]
        public void EnumResolver_Test(string type, ReportType expectedMapped)
        {
            var mapConfig = new MapperConfiguration(cfg => cfg.CreateMap<Report.Report, IReportConfiguration>(MemberList.Source)
                                                            .ForMember(dest => dest.PrinterName, opt => opt.MapFrom(x => x.PrinterName))
                                                            .ForMember(dest => dest.Type, opt => opt.MapFrom<EnumResolver>())
                                                            .ForSourceMember(src => src.Configuration, opt => opt.DoNotValidate())
                                                            );
            mapConfig.AssertConfigurationIsValid();
            var mapper = new Mapper(mapConfig);

            var deserializedReport = new Report.Report
            {
                Id = new System.Guid(),
                Type = type,
                Name = "name",
                PrinterName = "printername"
            };

            var mappedReportConfiguration = mapper.Map<Report.Report, IReportConfiguration>(deserializedReport);
            Assert.Equal(expectedMapped, mappedReportConfiguration.Type);
        }
    }
}
