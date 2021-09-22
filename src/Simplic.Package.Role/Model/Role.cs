using Newtonsoft.Json;
using System;

namespace Simplic.Package.Role
{
    /// <summary>
    /// Represents the content of a role.
    /// </summary>
    public class Role : IContent
    {
        /// <summary>
        /// Gets or sets the id.
        /// <para>
        /// Represents the unique identifier and primary key of role.
        /// </para>
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the internal name.
        /// </summary>
        public string InternalName { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }
    }
}