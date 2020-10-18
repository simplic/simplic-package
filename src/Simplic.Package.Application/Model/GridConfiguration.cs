namespace Simplic.Package.Application
{
    internal class GridConfiguration : IApplicationConfiguration
    {
        public string Grid { get; set; }
        public string Connection { get; set; }
        public bool LoadOnOpen { get; set; }
        public bool RefreshOnSelect { get; set; }
        public string SearchName { get; set; } = "";
    }
}