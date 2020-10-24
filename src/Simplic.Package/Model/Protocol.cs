using System.Collections.Generic;

namespace Simplic.Package
{
    public class Protocol
    {
        public IList<ProtocolItem> Messages { get; set; } = new List<ProtocolItem>();
    }
}