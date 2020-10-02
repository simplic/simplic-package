using System;

namespace Simplic.Package
{
    // TODO: Think of better name.
    //This wraps the content of an unpacked ObjectListItem (e.g. a deserialized Grid) with information for installing it
    public class InstallableObject
    {
        public string Target { get; set; }
        public IContent Content { get; set; }
        public Guid Guid { get; set; }
        public InstallMode Mode { get; set; }
        public string PackageName { get; set; }
        public Version PackageVersion { get; set; }
    }
}