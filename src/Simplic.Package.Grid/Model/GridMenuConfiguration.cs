using Simplic.Framework.DBUI;
using System;
using System.Windows.Input;
using Telerik.Windows.Controls.RibbonView;

namespace Simplic.Package.Grid
{
    public class GridMenuConfiguration
    {
        public Guid Id { get; set; }
        public Guid RightGuid { get; set; }
        public string DisplayName { get; set; }
        public string NamedEvent { get; set; }
        public bool IsRowBased { get; set; }
        public bool IsGridColumnBased { get; set; }
        public int DisplayIndex { get; set; }
        public bool AddToContextMenu { get; set; }
        public bool ExecuteOnDoubleClick { get; set; }
        public string ScriptName { get; set; }
        public string ScriptClass { get; set; }
        public string ScriptMethod { get; set; }
        public string NameParameterName { get; set; }
        public string ValueParameterName { get; set; }
        public Guid? SmallIconGuid { get; set; }
        public Guid? LargeIconGuid { get; set; }
        public ButtonSize RibbonIconSize { get; set; }
        public Key KeyCode1 { get; set; }
        public Key KeyCode2 { get; set; }
        public Key KeyCode3 { get; set; }
        public string ClrNamespace { get; set; }
        public string ClrClass { get; set; }
        public string ClrMethod { get; set; }
        public string QuickReportName { get; set; }
        public OpenReportViewerType ReportViewerType { get; set; }
        public bool PrintReport { get; set; }
        public bool IsCustomMenuEntry { get; set; }
        public GridMenuConfiguration ParentMenuEntry { get; set; }
    }
}