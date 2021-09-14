namespace Simplic.Package.Repository
{
    /// <summary>
    /// Represents the content of a repository.
    /// </summary>
    internal class RepositoryContent : IContent
    {
        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        public byte[] Data { get; set; }
    }
}