using Newtonsoft.Json;
using System;

namespace Simplic.Package.Icon
{
    public class IconContent : IContent
    {
        public string Name { get; set; }
        public byte[] Blob { get; set; }
    }
}