using System;
using System.Collections.Generic;

namespace Simplic.Package
{
    /// <summary>
    /// Represents a package.
    /// </summary>
    public class Package
    {
        /// <summary>
        /// Gets or sets the name.
        /// <para>
        /// The name is required and used to resolve dependencies.
        /// </para>
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a list of extensions for this package
        /// </summary>
        public IList<string> Extensions { get; set; }

        /// <summary>
        /// Gets or sets the guid.
        /// <para>
        /// Contains the uinique identifier of the package.
        /// </para>
        /// </summary>
        public Guid Guid { get; set; }

        /// <summary>
        /// Gets or sets the version.
        /// <para>
        /// Will conttain the version and is also a requirement to resolve dependencies.
        /// </para>
        /// </summary>
        public Version Version { get; set; }

        /// <summary>
        /// Gets or sets the dependencies.
        /// <para>
        /// The dependencies define which other packages need to be installed to be able to install this package.
        /// </para>
        /// </summary>
        public IList<Dependency> Dependencies { get; set; } = new List<Dependency>();

        /// <summary>
        /// Gets or sets the unpacked objects.
        /// <para>
        /// The unpacked objects contain the data contained in this package.
        /// </para>
        /// </summary>
        public IDictionary<string, IList<InstallableObject>> UnpackedObjects { get; set; } = new Dictionary<string, IList<InstallableObject>>();
    }
}