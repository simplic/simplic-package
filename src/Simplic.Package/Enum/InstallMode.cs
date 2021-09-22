namespace Simplic.Package
{
    /// <summary>
    /// Represents the result modes.
    /// <para>
    /// Used to decide wherther the object will be deployed or migrated.
    /// </para>
    /// </summary>
    public enum InstallMode
    {
        /// <summary>
        /// Deploys the installable object direktly.
        /// </summary>
        Deploy = 0,

        /// <summary>
        /// Before intallation the service will check wherther the migration will succeed.
        /// </summary>
        Migrate = 1
    }
}