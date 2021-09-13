using Newtonsoft.Json;
using System;

namespace Simplic.Package.Icon
{
    /// <summary>
    /// Represnts the content of an icon.
    /// </summary>
    public class IconContent : IContent
    {
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