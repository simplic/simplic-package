namespace Simplic.Package
{
    /// <summary>
    /// Represents the result of a method which validates a package configuration.
    /// <para>
    /// Contains additional information regarding the validation of the configuraion.
    /// </para>
    /// </summary>
    public class ValidatePackageConfigurationResult : LogResult
    {
        /// <summary>
        /// Gets or sets wherther the configuration is valid.
        /// </summary>
        public bool IsValid { get; set; }
    }
}