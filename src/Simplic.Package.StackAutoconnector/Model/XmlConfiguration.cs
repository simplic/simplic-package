using System;

namespace Simplic.Package.StackAutoconnector.Model
{
    /// <summary>
    /// Represents the content of a xml stack autoregister configuration.
    /// </summary>
    internal class XmlConfiguration : IStackAutoconnectorConfiguration
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the xml.
        /// </summary>
        public string Xml { get; set; } // Should this even be here?

        /// <summary>
        /// Gets or sets the descrition.
        /// </summary>
        public string Description { get; set; } = "";
    }
}