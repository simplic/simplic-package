using System;

namespace Simplic.Package.Model.Grid
{
    public class GridMenuConfiguration
    {
        public Guid Id { get; set; }
        public Guid RightGuid { get; set; }
        public string DisplayName { get; set; }
        public NameEvent {get; set;}
    public bool IsRowBased { get; set; }
    public bool IsGridColumnBased { get; set; }
    public short DisplayIndex { get; set; }
    public bool AddToContextMenu { get; set; }
    public bool ExecuteOnDoubleClick { get; set; }
    public ScriptName { get; set; }
public ScriptClass
{ get; set; }
public ScriptMethod
{ get; set; }
public NameParameterName
{ get; set; }
public ValueParameterName
{ get; set; }
public Guid SmallIconGuid { get; set; }
public Guid LargeIconGuid { get; set; }
public short RibbonIconSize { get; set; }
public short KeyCode1 { get; set; }
public short KeyCode2 { get; set; }
public short KeyCode3 { get; set; }
public string ClrNamespace { get; set; }
public string ClrClass { get; set; }
public string ClrMethod { get; set; }
public QuickReportName
{ get; set; }
public short ReportViewerType { get; set; }
public bool PrintReport { get; set; }
public bool IsCustomMenuEntry { get; set; }
public ParentMenuEntry
{ get; set; } 
    }
}
