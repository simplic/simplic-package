using Newtonsoft.Json;
using System;

namespace Simplic.Package.StackFulltext
{
    /// <summary>
    /// Represents the content of a stack fulltext.
    /// </summary>
    public class StackFulltext : IContent
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the stack id.
        /// </summary>
        public Guid StackId { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the configuration.
        /// </summary>
        public IStackFulltextConfiguration Configuration { get; set; }
    }
}