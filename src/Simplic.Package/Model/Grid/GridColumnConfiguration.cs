using System;
using System.ComponentModel;
using Simplic.UI.GridView;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Windows;
using Simplic.Framework.DBUI;

namespace Simplic.Package.Model.Grid
{
    public class GridColumnConfiguration : ColumnConfiguration
    {
        public IList<GridCellSqlHighlight> GridCellSqlHighlights { get; set; }
        public bool UseForUniqueFiltering { get; set; }
        public string SplitCharacter { get; set; }
}
}
