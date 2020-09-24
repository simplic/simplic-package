using System;
using System.ComponentModel;
using Simplic.UI.GridView;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Windows;

namespace Simplic.Package.Model.Grid
{
    public class GridColumnConfiguration
    {
        public IList<GridCellHighlight> GridCellSqlHighlights { get; set; }
        public bool UseForUniqueFiltering { get; set; }
        //TODO: Cant find in Simplic.UI public SplitCharacter
        public IList<Telerik.Windows.Data.EnumerableAggregateFunctionBase> AggregationFunctions { get; set; }
        public Guid Id { get; set; }
        public string AggregationText { get; set; }
        public bool IsActive { get; set; }
        public string DisplayName { get; set; }
        public string InternName { get; set; }
        public TextAlignment TextAlignment { get; set; }
        public TextWrapping TextWrapping { get; set; }
        public float DefaultWidth { get; set; }
        public float MaxWidth { get; set; }
        public float MinWidth { get; set; }
        public int? DefaultGroupIndex { get; set; }
        public ListSortDirection? DefaultSorting { get; set; }
        public int FontSize { get; set; }
        public string FontFamily { get; set; }
        public int FontStyle { get; set; }
        public int DisplayIndex { get; set; }
        public string DataFormatting { get; set; }
        public IDivergentColumnType DivergentColumnType { get; set; }
        public StyleSelector CustomCellSelector { get; set; }
        public bool IsReadOnly { get; set; }
        public GridColumnAggregationMode AggregationMode { get; set; }
        public bool AggregationShowMinValue { get; set; }
        public bool AggregationShowMaxValue { get; set; }
        public bool AggregationShowAvgValue { get; set; }
        public bool AggregationShowCountValue { get; set; }
        public bool AggregationShowSumValue { get; set; }
        public bool IsSearchable { get; set; }
        public SearchDataType SearchDataType { get; set; }
        public string ColorColumnName { get; set; }
}
}
