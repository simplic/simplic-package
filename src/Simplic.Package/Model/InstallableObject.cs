using System;

namespace Simplic.Package
{
    // TODO: Think of better name.
    /// <summary>
    /// Represents an unpacked ObbjectListItem with information regarding the installation of the content.
    /// </summary>
    public class InstallableObject
    {
        /// <summary>
        /// Gets or sets the target.
        /// </summary>
        public string Target { get; set; }

        /// <summary>
        /// Gets or sets the content.
        /// <para>
        /// Contains the data content of the installable object.
        /// </para>
        /// </summary>
        public IContent Content { get; set; }

        /// <summary>
        /// Gets or sets the Guid
        /// </summary>
        public Guid? Guid { get; set; }

        /// <summary>
        /// Gets or sets the install mode.
        /// <para>
        /// Dependent on the install mode a migratoin might be needed.
        /// </para>
        /// </summary>
        public InstallMode Mode { get; set; }

        /// <summary>
        /// Gets or sets the package guid.
        /// <para>
        /// References a <see cref="Package"/>
        /// </para>
        /// </summary>
        public Guid PackageGuid { get; set; }

        /// <summary>
        /// Gets or sets the package version.
        /// <para>
        /// Defines the Version of the package that will be installed.
        /// </para>
        /// </summary>
        public Version PackageVersion { get; set; }
    }
}