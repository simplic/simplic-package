namespace Simplic.Package
{
    /// <summary>
    /// Represents the result of a method which unpacks an package
    /// <para>
    /// Contains additional data from the unpacking process. Especially the installable object.
    /// </para>
    /// </summary>
    public class UnpackObjectResult : LogResult
    {
        /// <summary>
        /// Gets or sets the unpacked object.
        /// <para>
        /// The <see cref="InstallableObject"/> contains all information needed for the installation of the object.
        /// </para>
        /// </summary>
        public InstallableObject InstallableObject { get; set; }
    }
}