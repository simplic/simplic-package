namespace Simplic.Package.Application
{
    public class PythonConfiguration : IApplicationConfiguration
    {
        public string Path { get; set; }
        public string Class { get; set; }
        public string Method { get; set; }
    }
}