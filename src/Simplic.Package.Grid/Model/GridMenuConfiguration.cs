using Simplic.Framework.DBUI;
using System;
using System.Windows.Input;
using Telerik.Windows.Controls.RibbonView;

namespace Simplic.Package.Grid
{
    /// <summary>
    /// Represents the content of a grid menu configuration.
    /// </summary>
    public class GridMenuConfiguration
    {
        /// <summary>
        /// Gets or sets the id.
        /// <para>
        /// Represents the unique identifier and primary key of a grid menu configuration.
        /// </para>
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the right guid.
        /// </summary>
        public Guid RightGuid { get; set; }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the named event.
        /// </summary>
        public string NamedEvent { get; set; }

        /// <summary>
        /// Gets or sets the is row based event flag.
        /// </summary>
        public bool IsRowBased { get; set; }

        /// <summary>
        /// Gets or sets the is grid column based flag.
        /// </summary>
        public bool IsGridColumnBased { get; set; }

        /// <summary>
        /// Gets or sets the display index.
        /// </summary>
        public int DisplayIndex { get; set; }

        /// <summary>
        /// Gets or sets the aff to cotext menu flag.
        /// </summary>
        public bool AddToContextMenu { get; set; }

        /// <summary>
        /// Gets or sets the execute on double click flag.
        /// </summary>
        public bool ExecuteOnDoubleClick { get; set; }

        /// <summary>
        /// Gets or sets the script name.
        /// </summary>
        public string ScriptName { get; set; }

        /// <summary>
        /// Gets or sets the script class.
        /// </summary>
        public string ScriptClass { get; set; }

        /// <summary>
        /// Gets or sets the script method.
        /// </summary>
        public string ScriptMethod { get; set; }

        /// <summary>
        /// Gets or sets the name parameter name.
        /// </summary>
        public string NameParameterName { get; set; }

        /// <summary>
        /// Gets or sets the value parameter name.
        /// </summary>
        public string ValueParameterName { get; set; }

        /// <summary>
        /// Gets or sets the small icon id.
        /// </summary>
        public Guid? SmallIconGuid { get; set; }

        /// <summary>
        /// Gets or sets the large icon id.
        /// </summary>
        public Guid? LargeIconGuid { get; set; }

        /// <summary>
        /// Gets ot sets the ribbon icon size
        /// </summary>
        public ButtonSize RibbonIconSize { get; set; }

        /// <summary>
        /// Grts or sets the key code 1.
        /// </summary>
        public Key KeyCode1 { get; set; }

        /// <summary>
        /// Gets or sets the key code 2.
        /// </summary>
        public Key KeyCode2 { get; set; }

        /// <summary>
        /// Gets or sets the key code 3.
        /// </summary>
        public Key KeyCode3 { get; set; }

        /// <summary>
        /// Gets or sets the clr namespace.
        /// </summary>
        public string ClrNamespace { get; set; }

        /// <summary>
        /// Gets or sets the clr class.
        /// </summary>
        public string ClrClass { get; set; }

        /// <summary>
        /// Gets or sets the clr method.
        /// </summary>
        public string ClrMethod { get; set; }

        /// <summary>
        /// Gets or sets the quick report name.
        /// </summary>
        public string QuickReportName { get; set; }

        /// <summary>
        /// Gets or sets the report viewer type.
        /// </summary>
        public OpenReportViewerType ReportViewerType { get; set; }

        /// <summary>
        /// Gets or sets the print report flag.
        /// </summary>
        public bool PrintReport { get; set; }

        /// <summary>
        /// Gets or sets the is custom menu entry flag.
        /// </summary>
        public bool IsCustomMenuEntry { get; set; }

        /// <summary>
        /// Gets or sets the parent menu entry.
        /// </summary>
        public GridMenuConfiguration ParentMenuEntry { get; set; }

    }
}