using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Simplic.Package.StackContextArea
{
    /// <summary>
    /// Represents the content of a stack context area.
    /// </summary>
    public class StackContextArea : IContent
    {
        /// <summary>
        /// Gets or sets the id.
        /// <para>
        /// Represents the unique identifier and primary key of a stack context area.
        /// </para>
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the stack id.
        /// </summary>
        public Guid StackId { get; set; }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the configuration.
        /// </summary>
        public IStackContextAreaConfiguration Configuration { get; set; }

        /// <summary>
        /// Gets or sets the context of stacks.
        /// </summary>
        public IList<ContextOfStackItem> ContextOfStacks { get; set; } = new List<ContextOfStackItem>();

        /// <summary>
        /// Gets or sets the search name.
        /// </summary>
        public string SearchName { get; set; } = "";
    }
}