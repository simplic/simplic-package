using System.Collections;
using System.Collections.Generic;

namespace Simplic.Package
{
    /// <summary>
    /// Represents the result of a method that packs objects.
    /// </summary>
    public class PackObjectResult
    {
        /// <summary>
        /// Gets or sets the file.
        /// <para>
        /// Contains the packed object.
        /// </para>
        /// </summary>
        public byte[] File { get; set; }

        /// <summary>
        /// Gets or sets the location.
        /// <para>
        /// Will contain the path where the file is saved.
        /// </para>
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Gets or sets the payload
        /// </summary>
        public IDictionary<string, byte[]> Payload { get; set; } = new Dictionary<string, byte[]>();
    }
}