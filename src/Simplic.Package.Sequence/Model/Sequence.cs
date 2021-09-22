using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Simplic.Package.Sequence
{
    /// <summary>
    /// Represents the content of a seqeunce.
    /// </summary>
    public class Sequence : IContent
    {
        /// <summary>
        /// Gets or sets the id.
        /// <para>
        /// Represents the unique identifier and primary key of a sequence.
        /// </para>
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the internal name.
        /// </summary>
        public string InternalName { get; set; }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the format string.
        /// </summary>
        public string Format { get; set; }

        /// <summary>
        /// Gets or sets the counter.
        /// </summary>
        public IList<CounterItem> Counter { get; set; } = new List<CounterItem>();
    }
}