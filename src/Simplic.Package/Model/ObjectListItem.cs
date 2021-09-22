using System;
using System.Collections.Generic;

namespace Simplic.Package
{
    /// <summary>
    /// Represents an item of an object list.
    /// </summary>
    public class ObjectListItem
    {
        /// <summary>
        /// Gets or sets the source.
        /// <para>
        /// Contains the source object path.
        /// </para>
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// Gets or sets the target.
        /// <para>
        /// Contains the target object path.
        /// </para>
        /// </summary>
        public string Target { get; set; }

        /// <summary>
        /// Gets or sets the guid.
        /// <para>
        /// Contains the unique identifier.
        /// </para>
        /// </summary>
        public Guid? Guid { get; set; }

        /// <summary>
        /// Gets or sets the install mode.
        /// </summary>
        public InstallMode Mode { get; set; }

        /// <summary>
        /// Gets or sets the payloads
        /// <para>
        /// Contains the payload of the objects.
        /// </para>
        /// </summary>
        public IList<Payload> Payload { get; set; } = new List<Payload>();
    }
}