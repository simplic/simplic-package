using System;

namespace Simplic.Package.Icon
{
    public class DeserializedIcon : IContent
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public byte[] IconBlob { get; set; }
    }
}