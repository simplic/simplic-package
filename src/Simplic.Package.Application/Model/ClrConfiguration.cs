namespace Simplic.Package.Application
{
    public class ClrConfiguration : IApplicationConfiguration
    {
        public string Namespace { get; set; }
        public string Class { get; set; }
        public string Method { get; set; }
    }
}