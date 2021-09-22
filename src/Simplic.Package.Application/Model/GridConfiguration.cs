namespace Simplic.Package.Application
{
    /// <summary>
    /// Represents a application configuration for a grid application.
    /// </summary>
    internal class GridConfiguration : IApplicationConfiguration
    {
        /// <summary>
        /// Gets or sets the grid name.
        /// </summary>
        public string Grid { get; set; }

        /// <summary>
        /// Gets or sets the connection.
        /// </summary>
        public string Connection { get; set; }

        /// <summary>
        /// Gets or sets the load on open flag.
        /// </summary>
        public bool LoadOnOpen { get; set; }

        /// <summary>
        /// Gets or sets the refresh of select flag.
        /// </summary>
        public bool RefreshOnSelect { get; set; }

        /// <summary>
        /// Gets or sets the search name.
        /// </summary>
        public string SearchName { get; set; } = "";
    }
}