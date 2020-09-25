using Simplic.Package;
using System;
using System.Windows;
using System.Collections.Generic;
using Simplic.Framework.DBUI;
using Simplic.Package.Model.Grid;

namespace Simplic.Package
{
    public class DeserializedGrid : IContent
    {
        public Guid ExportId { get; set; }
        public Guid Id { get; set; }
        public IList<GridMenuConfiguration> MenuFunctions { get; set; }
        public string Name { get; set; }
        public IList<GridProfileConfiguration> Profiles { get; set; }
        public IList<GridProfileConfiguration> ProfilesToDelete { get; set; }
        public Visibility ReloadGridButtonVisibility { get; set; }
        public string RequiredRole { get; set; }
        public string SearchConfiguration { get; set; }
        public GridMenuConfiguration SelectedMenuFunction { get; set; }
        public GridProfileConfiguration SelectedProfile { get; set; }
        public Guid? SelectedStackId { get; set; }
    }
}