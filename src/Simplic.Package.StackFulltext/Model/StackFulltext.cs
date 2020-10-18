using Newtonsoft.Json;
using System;

namespace Simplic.Package.StackFulltext
{
    public class StackFulltext : IContent
    {
        [JsonProperty(Required = Required.Always)]
        public Guid Id { get; set; }
        public Guid StackId { get; set; }
        public string Type { get; set; }
        public IStackFulltextConfiguration Configuration { get; set; }
    }
}