namespace Simplic.Package
{
    /// <summary>
    /// Service to load package extensions.
    /// </summary>
    public interface IExtensionService
    {
        /// <summary>
        /// Loads a package extension.
        /// </summary>
        /// <param name="package"></param>
        void LoadExtensions(Package package);
    }
}
