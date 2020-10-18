using System;

namespace Simplic.Package.StackAutoconnector.Model
{
    internal class XmlConfiguration : IStackAutoconnectorConfiguration
    {
        public Guid Id { get; set; }
        public string Xml { get; set; } // Should this even be here?
        public string Description { get; set; } = "";
    }
}