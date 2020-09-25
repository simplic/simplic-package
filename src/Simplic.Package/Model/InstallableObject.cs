namespace Simplic.Package
{
    // TODO: Think of better name.
    //This wraps the content of an unpacked ObjectListItem (e.g. a deserialized Grid) with information for installing it
    public class InstallableObject
    {
        public string Target { get; set; }
        public IContent Content { get; set; }
    }
}