using Newtonsoft.Json;
using System;
using System.Net.Mime;

namespace Simplic.Package.Application
{
    /// <summary>
    /// Represents the content of a simplic application.
    /// </summary>
    public class Application : IContent
    {
        /// <summary>
        /// Gets or sets the id.
        /// <para>
        /// Represents the unique identifier and primary key of an application.
        /// </para>
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the application.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the shortcut.
        /// </summary>
        public string Shortcut { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the icon id.
        /// </summary>
        public Guid IconId { get; set; }

        /// <summary>
        /// Gets or sets the ribbon group id.
        /// </summary>
        public Guid? RibbonGroupId { get; set; }

        /// <summary>
        /// Gets or sets the application configuration.
        /// <para>
        /// The configuration is dependent from the type.
        /// </para>
        /// </summary>
        public IApplicationConfiguration Configuration { get; set; }
    }
}