using System.Collections.Generic;

namespace Simplic.Package
{
    /// <summary>
    /// Service to load package extensions.
    /// </summary>
    public interface IExtensionService
    {
        /// <summary>
        /// Loads a list of package extensions.
        /// </summary>
        /// <param name="extensions">A list of file names containing extensions.</param>
        void LoadExtensions(IList<string> extensions);

        /// <summary>
        /// Loads a list of extensions from their bynary file.
        /// </summary>
        /// <param name="asseblies">A dictionary containing the name and assembly as byte array.</param>
        void LoadExtensionsFromBinaries(IDictionary<string, byte[]> asseblies);
    }
}
