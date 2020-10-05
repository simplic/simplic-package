using System;

namespace Simplic.Package.Application
{
    public class DeserializedApplication : IContent
    {
        public Guid Id { get; set; }
        public string LocalizationKey { get; set; }
        public string Type { get; set; }
        public IApplicationSettings Settings { get; set; }
    }
}