namespace Simplic.Package.Application
{
    /// <summary>
    /// Represents an applicaton configuration for python applications.
    /// </summary>
    public class PythonConfiguration : IApplicationConfiguration
    {
        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the class.
        /// </summary>
        public string Class { get; set; }

        /// <summary>
        /// Gets or sets the method.
        /// </summary>
        public string Method { get; set; }
    }
}