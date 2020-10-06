using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package.Report
{
    public class KeyValueConfiguration : IReportConfiguration
    {
        public string Connection { get; set; }
        public string Provider { get; set; }
        public bool IsListBad { get; set; }
        public IList<KeyValueParameterItem> Parameter { get; set; }
    }
}
