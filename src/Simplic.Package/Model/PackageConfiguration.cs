using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Simplic.Package
{
    /// <summary>
    /// Represents a package configuration.
    /// <para>
    /// Defines the base structure needed to build and pack a <see cref="Package"/>.
    /// </para>
    /// </summary>
    public class PackageConfiguration
    {
        /// <summary>
        /// Gets or sets the package format version.
        /// <para>
        /// This will define with which package system version the package is build.
        /// </para>
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public Version PackageFormatVersion { get; set; }

        /// <summary>
        /// Gets or sets the guid.
        /// <para>
        /// Cotnains the unique indetifier which will also be used for the package.
        /// </para>
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public Guid Guid { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// <para>
        /// The name will define the package and is used to resolve dependencies.
        /// </para>
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Version.
        /// <para>
        /// The version will indicate how new the package is and will be required to resolve dependencies.
        /// </para>
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public Version Version { get; set; }

        /// <summary>
        /// Gets or sets the dependencies.
        /// <para>
        /// The dependencies will define which packages need to be installed to install the current.
        /// </para>
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public IList<Dependency> Dependencies { get; set; } = new List<Dependency>();

        /// <summary>
        /// Gets or sets a list of extensions for this package.
        /// </summary>
        public IList<string> Extensions { get; set; } = new List<string>();

        /// <summary>
        /// Gets or sets the objects
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public IDictionary<string, IList<ObjectListItem>> Objects { get; set; } = new Dictionary<string, IList<ObjectListItem>>();
    }
}