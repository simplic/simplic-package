using System;
using System.ComponentModel;

namespace Simplic.Package.Model.Grid
{
    public class ColumnConfiguration
    {
        public IList GridCellSqlHighlights { get; set; }
        public bool UseForUniqueFiltering { get; set; }
        public SplitCharacter
        public IList AggregationFunctions { get; set; }
        public Guid Id { get; set; }
        public string AggregationText { get; set; }
        public bool IsActive { get; set; }
        public string DisplayName { get; set; }
        public string InternName { get; set; }
        public short TextAlignment { get; set; }
        public short TextWrapping { get; set; }
        public float DefaultWidth { get; set; }
        public float MaxWidth { get; set; }
        public float MinWidth { get; set; }
        public int? DefaultGroupIndex { get; set; }
        public ListSortDirection? DefaultSorting { get; set; }
        public int FontSize { get; set; }
        public string FontFamily { get; set; }
        public short FontStyle { get; set; }
        public short DisplayIndex { get; set; }
        public string DataFormatting { get; set; }
        public DivergentColumnType { get; set; }
    public CustomCellSelector { get; set; }
public bool IsReadOnly { get; set; }
public AggregationMode
{ get; set; }
public bool AggregationShowMinValue { get; set; }
public bool AggregationShowMaxValue { get; set; }
public bool AggregationShowAvgValue { get; set; }
public bool AggregationShowCountValue { get; set; }
public bool AggregationShowSumValue { get; set; }
public bool IsSearchable { get; set; }
public short SearchDataType { get; set; }
public ColorColumnName
{ get; set; }
}
}
