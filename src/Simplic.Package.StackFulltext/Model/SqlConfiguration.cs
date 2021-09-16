namespace Simplic.Package.StackFulltext
{
    /// <summary>
    /// Content of a sql stack fulltext configuration.
    /// </summary>
    public class SqlConfiguration : IStackFulltextConfiguration
    {
        /// <summary>
        /// Gets or sets the statement.
        /// </summary>
        public string Statement { get; set; } // TODO: This is not uniform in the sample
    }
}