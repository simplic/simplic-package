using System.Collections.Generic;

namespace Simplic.Package.Report
{
    public class KeyValueConfiguration : IReportConfiguration
    {
        public string Connection { get; set; }
        public string Provider { get; set; }
        public bool IsListBased { get; set; }
        public IList<KeyValueParameterItem> Parameter { get; set; }
    }
}