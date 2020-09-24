using Simplic.Package;
using System;
using System.Windows;
using System.Collections.Generic;
using Simplic.Framework.DBUI;

namespace Simplic.Package
{
    public class DeserializedGrid : IContent
    {
        public Guid ExportId { get; set; }
        public Guid Id { get; set; }
        public IList<GridMenuConfigurationModel> MenuFunctions { get; set; }
        public string Name { get; set; }
        public IList<GridProfileConfigurationModel> Profiles { get; set; }
        public IList<GridProfileConfigurationModel> ProfilesToDelete { get; set; }
        public Visibility ReloadGridButtonVisibility { get; set; }
        public string RequiredRole { get; set; }
        public string SearchConfiguration { get; set; }
        public GridMenuConfigurationModel SelectedMenuFunction { get; set; }
        public GridProfileConfigurationModel SelectedProfile { get; set; }
        public Guid? SelectedStackId { get; set; }
    }
}