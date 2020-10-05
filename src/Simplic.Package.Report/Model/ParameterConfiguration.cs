using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package.Report
{
    public class ParameterConfiguration : ReportConfiguration
    {
        public IList<ParameterItem> Parameter { get; set; }
    }
}
