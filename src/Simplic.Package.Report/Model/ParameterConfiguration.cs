using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package.Report
{
    public class ParameterConfiguration : IReportConfiguration
    {
        public string Connection { get; set; }
        public string Provider { get; set; }
        public IList<string> Parameter { get; set; }
    }
}
