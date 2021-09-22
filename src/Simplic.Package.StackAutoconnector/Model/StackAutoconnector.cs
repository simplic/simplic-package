using System;

namespace Simplic.Package.StackAutoconnector
{
    /// <summary>
    /// Represents the content of a stack autoconnector.
    /// </summary>
    public class StackAutoconnector : IContent
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the stack id.
        /// </summary>
        public Guid StackId { get; set; }

        /// <summary>
        /// Gets or sets the target.
        /// </summary>
        public Guid Target { get; set; } // Should this be guid?

        /// <summary>
        /// Gets or sets the configuraion.
        /// </summary>
        public IStackAutoconnectorConfiguration Configuration { get; set; }
    }
}