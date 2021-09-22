using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Simplic.Package.Grid
{
    /// <summary>
    /// Represents the content of a grid.
    /// </summary>
    public class Grid : IContent
    {
        /// <summary>
        /// Gets or sets the id.
        /// <para>
        /// Represents the unique identifier and primary key of a grid.
        /// </para>
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the menu functions.
        /// </summary>
        public IList<GridMenuConfiguration> MenuFunctions { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the profiles.
        /// </summary>
        public IList<GridProfileConfiguration> Profiles { get; set; }

        /// <summary>
        /// Gets or sets the profiles to delete.
        /// </summary>
        public IList<GridProfileConfiguration> ProfilesToDelete { get; set; }

        /// <summary>
        /// Gets or sets the reload grid button visibility.
        /// </summary>
        public Visibility ReloadGridButtonVisibility { get; set; }

        /// <summary>
        /// Gets or sets the required role.
        /// </summary>
        public string RequiredRole { get; set; }

        /// <summary>
        /// Gets or sets the search configuration.
        /// </summary>
        public string SearchConfiguration { get; set; }

        /// <summary>
        /// Gets or sets the selected menu funcion.
        /// </summary>
        public GridMenuConfiguration SelectedMenuFunction { get; set; }

        /// <summary>
        /// Gets or sets the selected profile.
        /// </summary>
        public GridProfileConfiguration SelectedProfile { get; set; }

        /// <summary>
        /// Gets or sets the selected stack id.
        /// </summary>
        public Guid? SelectedStackId { get; set; }
    }
}