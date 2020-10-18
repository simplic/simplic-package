using System.Collections.Generic;

namespace Simplic.Package.Report
{
    public class KeyValueReport : DeserializedReport
    {
        public bool IsListBased { get; set; }
        public IList<KeyValueParameterItem> Parameter { get; set; }
    }
}