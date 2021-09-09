using System.Collections.Generic;

namespace Simplic.Package.Grid
{
    /// <summary>
    /// Represents a grid column configuration.
    /// </summary>
    public class GridColumnConfiguration : ColumnConfiguration
    {
        /// <summary>
        /// Gets or sets the grid cell sql highlights.
        /// </summary>
        public IList<GridCellSqlHighlight> GridCellSqlHighlights { get; set; }

        /// <summary>
        /// Gets or sets the use for unique filtering flag.
        /// </summary>
        public bool UseForUniqueFiltering { get; set; }

        /// <summary>
        /// Gets or sets hte split character.
        /// </summary>
        public string SplitCharacter { get; set; }
    }
}