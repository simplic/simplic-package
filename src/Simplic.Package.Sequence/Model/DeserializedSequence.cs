using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Simplic.Package.Sequence
{
    public class DeserializedSequence : IContent
    {
        [JsonProperty(Required = Required.Always)]
        public Guid Id { get; set; }
        public string InternalName { get; set; }
        public string DisplayName { get; set; }
        public string Format { get; set; }
        public IList<CounterItem> Counter { get; set; }
    }
}