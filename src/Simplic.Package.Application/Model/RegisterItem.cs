using System;

namespace Simplic.Package.Application
{
    /// <summary>
    /// Represents a register item.
    /// </summary>
    public class RegisterItem
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the register id.
        /// </summary>
        public Guid RegisterId { get; set; }

        /// <summary>
        /// Gets or sets the display name.
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
        /// Gets or sets the search name.
        /// </summary>
        public string SearchName { get; set; } = "";
    }
}