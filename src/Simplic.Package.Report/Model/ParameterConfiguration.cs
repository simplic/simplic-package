using System.Collections.Generic;

namespace Simplic.Package.Report
{
    public class ParameterConfiguration : IReportConfiguration
    {
        public string Connection { get; set; }
        public string Provider { get; set; }
        public IList<string> Parameter { get; set; }
    }
}