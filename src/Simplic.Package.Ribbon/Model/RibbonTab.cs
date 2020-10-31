using System;
using System.Collections.Generic;

namespace Simplic.Package.Ribbon
{
    /// <summary>
    /// Represents a ribbon tab
    /// </summary>
    public class RibbonTab : IContent
    {
        /// <summary>
        /// Gets or sets the ribbon id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the group name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the group order id
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Gets or sets all ribbon groups
        /// </summary>
        public IList<RibbonGroup> Groups { get; set; } = new List<RibbonGroup>();
    }
}
