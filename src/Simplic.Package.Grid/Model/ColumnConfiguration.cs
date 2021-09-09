using Simplic.UI.GridView;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Simplic.Package.Grid
{
    /// <summary>
    /// Represents a column configuration.
    /// </summary>
    public class ColumnConfiguration
    {
        /// <summary>
        /// Gets or sets the aggregation columns.
        /// </summary>
        public IList<Telerik.Windows.Data.EnumerableAggregateFunctionBase> AggregationFunctions { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets ors sets the aggregation text.
        /// </summary>
        public string AggregationText { get; set; }

        /// <summary>
        /// Gets or sets wherther the column is active.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the intern name.
        /// </summary>
        public string InternName { get; set; }

        /// <summary>
        /// Gets or sets the text allignment.
        /// </summary>
        public TextAlignment TextAlignment { get; set; }

        /// <summary>
        /// Gets or sets the text wrapping.
        /// </summary>
        public TextWrapping TextWrapping { get; set; }

        /// <summary>
        /// Gets or sets the default width.
        /// </summary>
        public float DefaultWidth { get; set; }

        /// <summary>
        /// Gets or sets the max width.
        /// </summary>
        public float MaxWidth { get; set; }

        /// <summary>
        /// Gets or sets the min width.
        /// </summary>
        public float MinWidth { get; set; }

        /// <summary>
        /// Gets or sets the default group index.
        /// </summary>
        public int? DefaultGroupIndex { get; set; }

        /// <summary>
        /// Gets or sets the default sorting.
        /// </summary>
        public ListSortDirection? DefaultSorting { get; set; }

        /// <summary>
        /// Gets or sets the font size.
        /// </summary>
        public int FontSize { get; set; }

        /// <summary>
        /// Gets or sets the font family.
        /// </summary>
        public string FontFamily { get; set; }

        /// <summary>
        /// Gets or sets the font style.
        /// </summary>
        public int FontStyle { get; set; }

        /// <summary>
        /// Gets or sets the display index.
        /// </summary>
        public int DisplayIndex { get; set; }

        /// <summary>
        /// Gets or sets the date formatting.
        /// </summary>
        public string DataFormatting { get; set; }

        /// <summary>
        /// Gets or sets the divergent column type.
        /// </summary>
        public DivergentColumnType DivergentColumnType { get; set; }

        /// <summary>
        /// Gets or sets the custom cell selector.
        /// </summary>
        public StyleSelector CustomCellSelector { get; set; }

        /// <summary>
        /// Gets or sets the is readonly flag.
        /// </summary>
        public bool IsReadOnly { get; set; }

        /// <summary>
        /// Gets or sets the aggregation mode.
        /// </summary>
        public GridColumnAggregationMode AggregationMode { get; set; }

        /// <summary>
        /// Gets or sets the aggregation show min value flag.
        /// </summary>
        public bool AggregationShowMinValue { get; set; }

        /// <summary>
        /// Gets or sets the aggregation show max value flag.
        /// </summary>
        public bool AggregationShowMaxValue { get; set; }

        /// <summary>
        /// Gets or sets the aggregation show avg value flag.
        /// </summary>
        public bool AggregationShowAvgValue { get; set; }

        /// <summary>
        /// Gets or sets the aggregation show count value flag.
        /// </summary>
        public bool AggregationShowCountValue { get; set; }

        /// <summary>
        /// Gets or sets the aggregation show sum value flag.
        /// </summary>
        public bool AggregationShowSumValue { get; set; }

        /// <summary>
        /// Gets or sets the is searchable flag.
        /// </summary>
        public bool IsSearchable { get; set; }

        /// <summary>
        /// Gets or sets the search data type.
        /// </summary>
        public SearchDataType SearchDataType { get; set; }

        /// <summary>
        /// Gets or sets the color column name.
        /// </summary>
        public string ColorColumnName { get; set; }
    }
}