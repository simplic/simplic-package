using System.Collections;
using System.Collections.Generic;

namespace Simplic.Package
{
    /// <summary>
    /// Reptesents a result of a mehtod which extracts an archive entry.
    /// <para>
    /// The data contained in this result is used to unpack object. For exampe the raw file content and further information.
    /// </para>
    /// </summary>
    public class ExtractArchiveEntryResult
    {
        /// <summary>
        /// Gets or sets the data.
        /// <para>
        /// Contains the raw file as a byte array.
        /// </para>
        /// </summary>
        public byte[] Data { get; set; }

        /// <summary>
        /// Gets or sets the location path.
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Gets or sets the install mode.
        /// </summary>
        public InstallMode Mode { get; set; }

        /// <summary>
        /// Gets or sets the payload
        /// </summary>
        public IDictionary<string, byte[]> Payload { get; set; }
    }
}