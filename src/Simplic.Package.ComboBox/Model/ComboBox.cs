namespace Simplic.Package.ComboBox
{
    /// <summary>
    /// Represents the content of a combo box.
    /// </summary>
    public class ComboBox : IContent
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the sql statement.
        /// </summary>
        public string SqlStatement { get; set; }

        /// <summary>
        /// Gets or sets the select column.
        /// </summary>
        public string SelectColumn { get; set; }

        /// <summary>
        /// Gets or sets the select column background.
        /// </summary>
        public string SelectColumnBackground { get; set; }

        /// <summary>
        /// Gets or sets the fs control name.
        /// </summary>
        public string FsControlName { get; set; }
    }
}