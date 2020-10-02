using System.Collections.Generic;

namespace Simplic.Package.Model.Grid
{
    public class GridColumnConfiguration : ColumnConfiguration
    {
        public IList<GridCellSqlHighlight> GridCellSqlHighlights { get; set; }
        public bool UseForUniqueFiltering { get; set; }
        public string SplitCharacter { get; set; }
    }
}