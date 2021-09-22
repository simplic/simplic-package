using Newtonsoft.Json;
using System;

namespace Simplic.Package.StackRegister
{
    /// <summary>
    /// Represents the content of a stack register.
    /// </summary>
    public class StackRegister : IContent
    {
        /// <summary>
        /// Gets or sets the id.
        /// <para>
        /// Represents the unique identifier 
        /// and primary key of a stack register.
        /// </para>
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the stack id.
        /// </summary>
        public Guid StackId { get; set; }

        /// <summary>
        /// Gets or sets the is active flag.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the configuration.
        /// </summary>
        public IStackRegisterConfiguration Configuration { get; set; }
    }
}