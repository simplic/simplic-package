using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Simplic.Package.StackContextArea
{
    public class StackContextArea : IContent
    {
        [JsonProperty(Required = Required.Always)]
        public Guid Id { get; set; }
        public Guid StackId { get; set; }
        public string DisplayName { get; set; }
        public string Type { get; set; }
        public IStackContextAreaConfiguration Configuration { get; set; }
        public IList<ContextOfStackItem> ContextOfStacks { get; set; } = new List<ContextOfStackItem>();
        public string SearchName { get; set; } = "";
    }
}