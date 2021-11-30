namespace Simplic.Package.Configuration
{
    /// <summary>
    /// Defines the source of the value.
    /// </summary>
    public enum ConfigurationValueSource
    {
        /// <summary>
        /// A package value is the value in the package.json.
        /// </summary>
        PackageValue = 0,

        /// <summary>
        /// A request value is requested in every installation.
        /// </summary>
        RequestValue = 1
    }
}
