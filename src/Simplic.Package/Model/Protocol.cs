using System.Collections.Generic;

namespace Simplic.Package
{
    /// <summary>
    /// Represents a protocol.
    /// </summary>
    public class Protocol
    {
        /// <summary>
        /// Gets or sets the messages.
        /// <para>
        /// Will contain all logged messages of a executed package method.
        /// </para>
        /// </summary>
        public IList<ProtocolItem> Messages { get; set; } = new List<ProtocolItem>();
    }
}