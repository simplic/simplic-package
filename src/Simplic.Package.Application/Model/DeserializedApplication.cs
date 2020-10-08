using Newtonsoft.Json;
using System;
using System.Net.Mime;

namespace Simplic.Package.Application
{
    public class DeserializedApplication : IContent
    {
        [JsonProperty(Required = Required.Always)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Shortcut { get; set; }
        public string Type { get; set; }
        public Guid IconId { get; set; }
        public Guid RibbonGroupId { get; set; }
        public IApplicationConfiguration Configuration { get; set; }
    }
}