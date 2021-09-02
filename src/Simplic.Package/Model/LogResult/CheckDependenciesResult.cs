using System.Collections.Generic;

namespace Simplic.Package
{
    /// <summary>
    /// Represents a log result of a mehtod which checks dependencies.
    /// <para>
    /// Will contain further information about missing dependencies.
    /// </para>
    /// </summary>
    public class CheckDependenciesResult : LogResult
    {
        /// <summary>
        /// Gets or sets the list of missing dependencies.
        /// <para>
        /// Will contain a list of <see cref="Dependency"/>.
        /// The list will be filled in methods which check the dependencies needed to install a certain package.
        /// </para>
        /// </summary>
        public IList<Dependency> MissingDependencies { get; set; }
    }
}