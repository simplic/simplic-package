using Simplic.Package.Model.Grid;
using System;
using System.Collections.Generic;

namespace Simplic.Package
{
    public class DeserializedGrid : IDeserializedContent
    {
        public Guid ExportId { get; set; }
        public Guid Id { get; set; }
        public IList<GridMenuConfiguration> MenuFunctions { get; set; }
        public string Name { get; set; }
        public IList<GridProfileConfiguration> Profiles { get; set; }
        public IList<GridProfileConfiguration> ProfilesToDelete { get; set; }
        public ReloadGridButtonVisibility { get; set; }
    public string RequiredRole { get; set; }
    public string SearchConfiguration { get; set; }
    public GridMenuConfiguration SelectedMenuFunction { get; set; }
    public GridProfileConfiguration SelectedProfile { get; set; }
    public Guid? SelectedStackId { get; set; }
}
}