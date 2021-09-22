using System.Collections.Generic;

namespace Simplic.Package.Application
{
    /// <summary>
    /// Represents a application configuration for grid structure applications.
    /// </summary>
    public class GridStructureConfiguration : IApplicationConfiguration
    {
        /// <summary>
        /// Gets or sets a list of stacks.
        /// </summary>
        public IList<StackItem> Stacks { get; set; } = new List<StackItem>();
    }
}