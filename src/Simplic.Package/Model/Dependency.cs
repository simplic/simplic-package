using System;

namespace Simplic.Package
{
    /// <summary>
    /// Represents a dependencie of a package.
    /// <para>
    /// Packages containing a dependency need to have the dependend package installed before with the version mentioned in the Dependency object.
    /// </para>
    /// </summary>
    public class Dependency
    {
        /// <summary>
        /// Gets or sets the package name.
        /// <para>
        /// This will be the primary indicator wherther a dependent package is already installed.
        /// </para>
        /// </summary>
        public string Package { get; set; }

        /// <summary>
        /// Gets or sets the version of the dependent package.
        /// <para>
        /// Relative to the <see cref="GreaterAllowed"/> property either the exact version or a higher one needs to be insalled.
        /// </para>
        /// </summary>
        public Version Version { get; set; }

        /// <summary>
        /// Gets or sets whether a greather version is allowed.
        /// <para>
        /// When no greater version is allowed the package can not be installed when a newer that the dependent version is installed.
        /// </para>
        /// <para>
        /// The default value is true, meaning that greater/newer versions are allowed.
        /// </para>
        /// </summary>
        public bool GreaterAllowed { get; set; } = true;
    }
}