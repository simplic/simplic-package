using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Simplic.Package
{
    public class PackageConfiguration
    {
        [JsonProperty(Required = Required.Always)]
        public Version PackageFormatVersion { get; set; }
        
        [JsonProperty(Required = Required.Always)]
        public Guid Guid { get; set; }

        [JsonProperty(Required = Required.Always)]
        public string Name { get; set; }

        [JsonProperty(Required = Required.Always)]
        public Version Version { get; set; }

        [JsonProperty(Required = Required.Always)]
        public IList<Dependency> Dependencies { get; set; }

        [JsonProperty(Required = Required.Always)]
        public IDictionary<string, IList<ObjectListItem>> Objects { get; set; }
    }
}