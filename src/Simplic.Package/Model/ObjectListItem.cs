using System;

namespace Simplic.Package
{
    public class ObjectListItem
    {
        public string Source { get; set; }
        public string Target { get; set; }
        public Guid Guid { get; set; }
        public InstallMode Mode { get; set; }
    }
}