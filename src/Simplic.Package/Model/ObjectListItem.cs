namespace Simplic.Package
{
    public class ObjectListItem
    {
        public string Source { get; set; }
        public string Target { get; set; }
        public bool Deserialize { get; set; }
        public MigrationMode Mode{ get; set; } // TODO: Does this also define whether an object has to be deserialized?
    }
}