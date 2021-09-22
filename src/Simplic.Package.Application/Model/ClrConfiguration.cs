namespace Simplic.Package.Application
{
    /// <summary>
    /// Represents the application configuration of an clr apllication.
    /// </summary>
    public class ClrConfiguration : IApplicationConfiguration
    {
        /// <summary>
        /// Gets or sets the namespace.
        /// </summary>
        public string Namespace { get; set; }

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