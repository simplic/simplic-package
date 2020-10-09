using System;
using System.Windows.Media;

namespace Simplic.Package.Grid
{
    public class GridCellSqlHighlight
    {
        public Guid Id { get; set; }
        public string CompareValue { get; set; }
        public Color BackgroundColor { get; set; }
        public Color ForegroundColor { get; set; }
    }
}