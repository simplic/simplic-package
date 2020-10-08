using AutoMapper;
using Simplic.Reporting;
using System.Collections.Generic;

namespace Simplic.Package.Report
{
    public class EnumResolver : IValueResolver<DeserializedReport, Simplic.Reporting.IReportConfiguration, ReportType>
    {
        public ReportType Resolve(DeserializedReport source, Simplic.Reporting.IReportConfiguration destination, ReportType destMember, ResolutionContext context)
        {
            var enumDict = new Dictionary<string, ReportType>
            {
                {"sql", ReportType.SqlReport },
                {"key-value", ReportType.KeyValueReport},
                {"parameter", ReportType.ParameterReport}
            };

            return enumDict[source.Type];
        }
    }
}
