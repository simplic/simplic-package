using System;

namespace Simplic.Package.StackAutoconnector
{
    public class StackAutoconnector : IContent
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public Guid StackId { get; set; }
        public Guid Target { get; set; } // Should this be guid?
        public IStackAutoconnectorConfiguration Configuration { get; set; }
    }
}