using System;

namespace Simplic.Package.Application
{
    public class RegisterItem
    {
        public Guid Id { get; set; }
        public Guid RegisterId { get; set; }
        public string DisplayName { get; set; }
        public string Grid { get; set; }
        public int OrderId { get; set; }
    }
}