using System;
using System.Windows.Media;

namespace Simplic.Package.Grid
{
    /// <summary>
    /// Represents a grid cell sql highlight.
    /// </summary>
    public class GridCellSqlHighlight
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the compare value.
        /// </summary>
        public string CompareValue { get; set; }

        /// <summary>
        /// Gets or sets the background color.
        /// </summary>
        public Color BackgroundColor { get; set; }

        /// <summary>
        /// Gets or sets the foreground color.
        /// </summary>
        public Color ForegroundColor { get; set; }
    }
}