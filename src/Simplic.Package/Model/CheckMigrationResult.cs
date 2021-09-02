namespace Simplic.Package
{
    /// <summary>
    /// Represents a log result of a method which checks wherther a migration can be done.
    /// <para>
    /// Contains additional properties for information wherther a migration can be executed.
    /// </para>
    /// </summary>
    public class CheckMigrationResult : LogResult
    {
        /// <summary>
        /// Gets or sets wherther the package can be migrated in the current installation.
        /// </summary>
        public bool CanMigrate { get; set; }
    }
}