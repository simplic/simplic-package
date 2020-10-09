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
        [Fact]
        public void InstallObject_Test()
        {
        }

        [Theory]
        [InlineData("key-value", Reporting.ReportType.KeyValueReport)]
        [InlineData("sql", Reporting.ReportType.SqlReport)]
        [InlineData("parameter", Reporting.ReportType.ParameterReport)]
        public void EnumResolver_Test(string type, Reporting.ReportType expectedMapped)
        {
            var mapConfig = new MapperConfiguration(cfg => cfg.CreateMap<DeserializedReport, Simplic.Reporting.IReportConfiguration>(MemberList.Source)
                                                            .ForMember(dest => dest.ReportName, opt => opt.MapFrom(x => x.ReportFile))
                                                            .ForMember(dest => dest.Type, opt => opt.MapFrom<EnumResolver>())
                                                            .ForSourceMember(src => src.Configuration, opt => opt.DoNotValidate())
                                                            );
            mapConfig.AssertConfigurationIsValid();
            var mapper = new Mapper(mapConfig);

            var deserializedReport = new DeserializedReport
            {
                Id = new System.Guid(),
                Type = type,
                Name = "name",
                ReportFile = "reportfilename"
            };

            var mappedReportConfiguration = mapper.Map<DeserializedReport, Simplic.Reporting.IReportConfiguration>(deserializedReport);
            Assert.Equal(expectedMapped, mappedReportConfiguration.Type);
        }

        [Fact]
        public void Map_Test()
        {
            var report = new DeserializedReport
            {
                Id = new System.Guid(),
                Type = "key-value",
                Name = "name",
                ReportFile = "reportfilename",
                Configuration = new KeyValueConfiguration
                {
                    Connection = "f",
                    Provider = "asdf",
                    IsListBased = true,
                    Parameter = new List<KeyValueParameterItem>
                    {
                        new KeyValueParameterItem
                        {
                            Name = "k",
                            OrderId = 1
                        }
                    }
                }
            };

            if (report.Configuration is KeyValueConfiguration configuration)
            {
                var mapConfig = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<KeyValueParameterItem, KeyValueParameterConfigurationModel>(MemberList.Source)
                        .ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.Name))
                        .ForMember(dest => dest.OrderId, opt => opt.MapFrom(x => x.OrderId));
                    cfg.CreateMap<DeserializedReport, KeyValueConfigurationModel>(MemberList.Source)
                        .ForMember(dest => dest.ReportName, opt => opt.MapFrom(x => x.ReportFile))
                        .ForMember(dest => dest.Type, opt => opt.MapFrom<EnumResolver>())
                        .ForMember(dest => dest.Parameter, opt => opt.MapFrom(x => configuration.Parameter))
                        .ForMember(dest => dest.ConnectionString, opt => opt.MapFrom(x => x.Configuration.Connection))
                        .ForMember(dest => dest.PrinterName, opt => opt.MapFrom(x => x.Configuration.Provider));
                });
                mapConfig.AssertConfigurationIsValid();

                var mapper = new Mapper(mapConfig);
                var res = mapper.Map<KeyValueConfigurationModel>(report);
            }

           
        }
    }
}
