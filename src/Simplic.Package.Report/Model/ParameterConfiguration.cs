using System.Collections.Generic;

namespace Simplic.Package.Report
{
    public class ParameterConfiguration : ReportConfiguration
    {
        public IList<string> Parameter { get; set; } = new List<string>();
    }
}