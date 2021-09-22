using System;

namespace Simplic.Package.Sequence
{
    /// <summary>
    /// Represents the content of a counter item.
    /// </summary>
    public class CounterItem
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the valid from date.
        /// </summary>
        public DateTime? ValidFrom { get; set; }

        /// <summary>
        /// Gets or sets the valid to date.
        /// </summary>
        public DateTime? ValidTo { get; set; }

        /// <summary>
        /// Gets or sets the min counter value.
        /// </summary>
        public int Min { get; set; }

        /// <summary>
        /// Gets or sets the max counter value.
        /// </summary>
        public int Max { get; set; }

        /// <summary>
        /// Gets or sets the step size.
        /// </summary>
        public int Step { get; set; }

        /// <summary>
        /// Gets or setst the fixed length.
        /// </summary>
        public int FixedLength { get; set; }

        /// <summary>
        /// Gets or sets the format flag.
        /// </summary>
        public bool Format { get; set; }

        /// <summary>
        /// Gets or sets the optional format string.
        /// </summary>
        public string OptionalFormat { get; set; }

        /// <summary>
        /// Gets or sets the tenant id.
        /// </summary>
        public Guid? TenantId { get; set; }
    }
}