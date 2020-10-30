using System.Collections.Generic;

namespace Simplic.Package.Report
{
    public class KeyValueConfiguration : ReportConfiguration
    {
        public bool IsListBased { get; set; }
        public IList<KeyValueParameterItem> Parameter { get; set; } = new List<KeyValueParameterItem>();
    }
}