using AutoMapper;
using Simplic.Reporting;
using System.Collections.Generic;
using System.Windows.Documents;
using System.Xaml;
using Xunit;

namespace Simplic.Package.Report
{
    public class ReportRepositoryTest
    {
        [Theory]
        [InlineData("key-value", Reporting.ReportType.KeyValueReport)]
        [InlineData("sql", Reporting.ReportType.SqlReport)]
        [InlineData("parameter", Reporting.ReportType.ParameterReport)]
        public void EnumResolver_Test(string type, Reporting.ReportType expectedMapped)
        {
            var mapConfig = new MapperConfiguration(cfg => cfg.CreateMap<Report, Simplic.Reporting.IReportConfiguration>(MemberList.Source)
                                                            .ForMember(dest => dest.ReportName, opt => opt.MapFrom(x => x.ReportFile))
                                                            .ForMember(dest => dest.Type, opt => opt.MapFrom<EnumResolver>())
                                                            .ForSourceMember(src => src.Configuration, opt => opt.DoNotValidate())
                                                            );
            mapConfig.AssertConfigurationIsValid();
            var mapper = new Mapper(mapConfig);

            var deserializedReport = new Report
            {
                Id = new System.Guid(),
                Type = type,
                Name = "name",
                ReportFile = "reportfilename"
            };

            var mappedReportConfiguration = mapper.Map<Report, Simplic.Reporting.IReportConfiguration>(deserializedReport);
            Assert.Equal(expectedMapped, mappedReportConfiguration.Type);
        }
    }
}
