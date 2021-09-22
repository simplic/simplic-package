namespace Simplic.Package
{
    /// <summary>
    /// Represents a result of a method which uninstalls a package.
    /// <para>
    /// Contains additional data rearding the success of the uninstalling process.
    /// </para>
    /// </summary>
    public class UninstallObjectResult : LogResult
    {
        /// <summary>
        /// Gets or sets wherther the uninstall was successful.
        /// </summary>
        public bool Success { get; set; }
    }
}