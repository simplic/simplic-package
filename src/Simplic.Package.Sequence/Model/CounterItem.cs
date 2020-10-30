using System;

namespace Simplic.Package.Sequence
{
    public class CounterItem
    {
        public Guid Id { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        public int Step { get; set; }
        public int FixedLength { get; set; }
        public bool Format { get; set; }
        public string OptionalFormat { get; set; }
        public Guid? TenantId { get; set; }
    }
}