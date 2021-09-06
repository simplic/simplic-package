using System;
using System.Collections.Generic;

namespace Simplic.Package.Application
{
    /// <summary>
    /// Represents a stack item.
    /// </summary>
    public class StackItem
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the stack id.
        /// </summary>
        public Guid StackId { get; set; }

        /// <summary>
        /// Gets or sets the is visible flag.
        /// </summary>
        public bool IsVisible { get; set; }

        /// <summary>
        /// Gets or sets the display name
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the grid name.
        /// </summary>
        public string Grid { get; set; }

        /// <summary>
        /// Gets or sets the order id.
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Gets or sets a list of registers.
        /// </summary>
        public IList<RegisterItem> Registers { get; set; } = new List<RegisterItem>();

        /// <summary>
        /// Gets or sets the search name.
        /// </summary>
        public string SearchName { get; set; } = "";
    }
}