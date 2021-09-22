using System;

namespace Simplic.Package.StackContextArea
{
    /// <summary>
    /// Represents a statement parameter.
    /// </summary>
    internal class StatementParam
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
        /// Gets or sets the display name.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the grid name.
        /// </summary>
        public string GridName { get; set; }

        /// <summary>
        /// Gets or sets the stack based flag.
        /// </summary>
        public bool StackBased { get; set; }

        /// <summary>
        /// Gets or sets the connect with archive flag.
        /// </summary>
        public bool ConnectWithArchive { get; set; }

        /// <summary>
        /// Gets or sets the search name.
        /// </summary>
        public string SearchName { get; set; }
    }
}