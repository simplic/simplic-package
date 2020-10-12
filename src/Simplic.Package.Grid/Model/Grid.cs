using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Simplic.Package.Grid
{
    public class Grid : IContent
    {
        [JsonProperty(Required = Required.Always)]
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