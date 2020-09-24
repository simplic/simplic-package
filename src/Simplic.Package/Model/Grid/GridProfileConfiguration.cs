using System;
using System.Collections.Generic;
using System.Windows;
namespace Simplic.Package.Model.Grid
{
    public class GridProfileConfiguration
    {
        public string ProfileName { get; set; }
        public int DisplayIndex { get; set; }
        public string RequiredRole { get; set; }
        public string SearchConfiguration { get; set; }
        public string DocumentViewerName { get; set; }
        public bool IsDefault { get; set; }
        public bool ShowNumerationColumn { get; set; }
        public bool UseSelectColumn { get; set; }
        public IList<ColumnConfiguration> ColumnConfiguration { get; set; }
        public Guid Id { get; set; }
        public string SelectStatements { get; set; }
        public bool LoadAllData { get; set; }
        public bool ShouldAutoReload { get; set; }
        public int? AutoReloadInterval { get; set; }
        public byte[] UserSettings { get; set; } // This should be a base64 encoded string in the json. Can it still be deserialized implicitly?
        public bool IsCustomProfile { get; set; }
        public bool IsDeactivated { get; set; }
        public bool EnableGridRefreshOnDataChange { get; set; }
        public string ResolveDroppedDataStatement { get; set; }
        public bool UseHierarchicalGrid { get; set; }
        public string HierachicalGridName { get; set; }
        public IList<GridVirtualGroupConfiguration> VirtualGroupDefinitions { get; set; }
        public bool CollapseAfterExecute { get; set; }
        public GridVirtualGroupConfiguration SelectedVirtualGroup { get; set; }
        public ColumnConfiguration SelectedColumn { get; set; }
        public bool ShowColumnFooter { get; set; }
        public string CreateClrAPIModelMethod { get; set; }
        public string CreateClrAPIModelMethodParameter { get; set; }
        public bool EnableDrag { get; set; }
        public bool EnableDrop { get; set; }
        public Func<object, System.Windows.Style> CustomRowSelector { get; set; } // [public Func<object, System.Windows.Style> CustomRowSelector] Wie werden delegates serialisiert?
        public bool AutoExpandGroups { get; set; }
        public string DropDataService { get; set; }
        public bool RemoveFromDragSrouce { get; set; }
        public bool EnableStickyGroupHeaders { get; set; }
        public string GroupConfigurationJson { get; set; }
        public IList<GridVirtualGroupConfiguration> VirtualGroups { get; set; }
    }
}
