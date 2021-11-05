using Newtonsoft.Json;
using System;

namespace Simplic.Package.Icon
{
    /// <summary>
    /// Represents the content of an icon.
    /// </summary>
    public class IconContent : IContent
    {
        /// <summary>
        /// Gets or sets the Guid
        /// <para>
        /// Represent the primary key and unique identifier of an icon.
        /// </para>
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public Guid Guid { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the blob.
        /// </summary>
        public byte[] Blob { get; set; }
    }
}