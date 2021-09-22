namespace Simplic.Package.ItemBox
{

    /// <summary>
    /// Represents the content of an itembox profile.
    /// </summary>
    public class ItemBoxProfile
    {
        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the type
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the grid.
        /// </summary>
        public string Grid { get; set; }

        /// <summary>
        /// Gets or sets the is active flag.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the default search string. 
        /// </summary>
        public string DefaultSearchString { get; set; }

        /// <summary>
        /// Gets or sets the regex.
        /// </summary>
        public string Regex { get; set; }

        /// <summary>
        /// Gets or sets the select statement.
        /// </summary>
        public string SelectStatement { get; set; }
    }
}