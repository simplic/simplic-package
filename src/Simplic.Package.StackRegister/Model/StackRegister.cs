using Newtonsoft.Json;
using System;

namespace Simplic.Package.StackRegister
{
    public class StackRegister : IContent
    {
        [JsonProperty(Required = Required.Always)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid StackId { get; set; }
        public bool IsActive { get; set; }
        public string Type { get; set; }
        public IStackRegisterConfiguration Configuration { get; set; }
    }
}