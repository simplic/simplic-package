using System;
using System.Drawing;

namespace Simplic.Package.Model.Grid
{
    public class GridCellSqlHighlight
    {
        public Guid Id { get; set; }
        public string CompareValue { get; set; }
        public string CompareColumnName { get; set; }
        public Color BackgroundColor { get; set; }
        public Color ForegroundColor { get; set; }
    }
}