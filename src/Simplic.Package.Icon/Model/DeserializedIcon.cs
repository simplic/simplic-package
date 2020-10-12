using Newtonsoft.Json;
using System;

namespace Simplic.Package.Icon
{
    public class DeserializedIcon : IContent
    {
        [JsonProperty(Required = Required.Always)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public byte[] Blob { get; set; }
    }
}