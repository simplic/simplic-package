using System.Collections.Generic;

namespace Simplic.Package.StackContextArea
{
    /// <summary>
    /// Represents the content of a grid configuration.
    /// </summary>
    internal class GridConfiguration : IStackContextAreaConfiguration
    {
        /// <summary>
        /// Gets or sets the name of the grid.
        /// </summary>
        public string Grid { get; set; }

        /// <summary>
        /// Gets or sets the stack based flag.
        /// </summary>
        public bool StackBased { get; set; }

        /// <summary>
        /// Gets or sets the connect with archive flag.
        /// </summary>
        public bool ConnectWithArchive { get; set; }
    }
}