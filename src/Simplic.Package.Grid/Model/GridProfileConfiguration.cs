using System;
using System.Collections.Generic;

namespace Simplic.Package.Grid
{
    /// <summary>
    /// Contains the content of a grid profile configuration.
    /// </summary>
    public class GridProfileConfiguration
    {
        /// <summary>
        /// Gets or sets the profile name.
        /// </summary>
        public string ProfileName { get; set; }

        /// <summary>
        /// Gets or sets the display index.
        /// </summary>
        public int DisplayIndex { get; set; }

        /// <summary>
        /// Gets or sets the required role.
        /// </summary>
        public string RequiredRole { get; set; }

        /// <summary>
        /// Gets or sets the search configuration.
        /// </summary>
        public string SearchConfiguration { get; set; }

        /// <summary>
        /// Gets or sets the document viewer name.
        /// </summary>
        public string DocumentViewerName { get; set; }

        /// <summary>
        /// Gets or sets the is default flag.
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// Gets or sets the show numeration column flag.
        /// </summary>
        public bool ShowNumerationColumn { get; set; }

        /// <summary>
        /// Gets or sets the use selectrd column flag.
        /// </summary>
        public bool UseSelectColumn { get; set; }

        /// <summary>
        /// Gets or sets the column configuration.
        /// </summary>
        public IList<GridColumnConfiguration> ColumnConfiguration { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the select statement.
        /// </summary>
        public string SelectStatement { get; set; }

        /// <summary>
        /// Gets or sets the load all data flag.
        /// </summary>
        public bool LoadAllData { get; set; }

        /// <summary>
        /// Gets or sets the should auto reload flag.
        /// </summary>
        public bool ShouldAutoReload { get; set; }

        /// <summary>
        /// Gets or sets the auto reload interval.
        /// </summary>
        public int? AutoReloadInterval { get; set; }

        /// <summary>
        /// Gets or sets the user settings.
        /// </summary>
        public byte[] UserSettings { get; set; } // This should be a base64 encoded string in the json. Can it still be deserialized implicitly?

        /// <summary>
        /// Gets or sets the id custom profile flag.
        /// </summary>
        public bool IsCustomProfile { get; set; }

        /// <summary>
        /// Gets or sets the is deactivated flag.
        /// </summary>
        public bool IsDeactivated { get; set; }

        /// <summary>
        /// Gets or sets the enable grid refresh on data change flag.
        /// </summary>
        public bool EnableGridRefreshOnDataChange { get; set; }

        /// <summary>
        /// Gets or sets the resolve dropped data statement.
        /// </summary>
        public string ResolveDroppedDataStatement { get; set; }

        /// <summary>
        /// Gets or sets the use hierachical grid flag.
        /// </summary>
        public bool UseHierarchicalGrid { get; set; }

        /// <summary>
        /// Gets or sets the hierachical grid name.
        /// </summary>
        public string HierachicalGridName { get; set; }

        /// <summary>
        /// Gets or sets the virtual group definitions.
        /// </summary>
        public IList<VirtualGroupDefinition> VirtualGroupDefinitions { get; set; }

        /// <summary>
        /// Gets or sets the collapse after execute flag.
        /// </summary>
        public bool CollapseAfterExecute { get; set; }

        /// <summary>
        /// Gets or sets the selceted virual group.
        /// </summary>
        public GridVirtualGroupConfiguration SelectedVirtualGroup { get; set; }

        /// <summary>
        /// Gets or sets the selected column.
        /// </summary>
        public ColumnConfiguration SelectedColumn { get; set; }

        /// <summary>
        /// Gets or sets the show column footer.
        /// </summary>
        public bool ShowColumnFooter { get; set; }

        /// <summary>
        /// Gets or sets the create clr api method.
        /// </summary>
        public string CreateClrAPIModelMethod { get; set; }

        /// <summary>
        /// Gets or sets the create clr api model method parameter.
        /// </summary>
        public string CreateClrAPIModelMethodParameter { get; set; }

        /// <summary>
        /// Gets or sets the enable drag flag.
        /// </summary>
        public bool EnableDrag { get; set; }

        /// <summary>
        /// Gets or sets the enable drop flag.
        /// </summary>
        public bool EnableDrop { get; set; }

        /// <summary>
        /// Gets or sets the custom row selector.
        /// </summary>
        public Func<object, System.Windows.Style> CustomRowSelector { get; set; } // [public Func<object, System.Windows.Style> CustomRowSelector] Wie werden delegates serialisiert?

        /// <summary>
        /// Gets or sets the auto expand groups.
        /// </summary>
        public bool AutoExpandGroups { get; set; }

        /// <summary>
        /// Gets or sets the drop data service.
        /// </summary>
        public string DropDataService { get; set; }

        /// <summary>
        /// Gets or sets the remove from drag source.
        /// </summary>
        public bool RemoveFromDragSrouce { get; set; }

        /// <summary>
        /// Gets or sets the enable sticky group headers flag.
        /// </summary>
        public bool EnableStickyGroupHeaders { get; set; }

        /// <summary>
        /// Gets or sets the group configuration json.
        /// </summary>
        public byte[] GroupConfigurationJson { get; set; }

        /// <summary>
        /// Gets or sets the virtual groups.
        /// </summary>
        public IList<GridVirtualGroupConfiguration> VirtualGroups { get; set; }
    }
}