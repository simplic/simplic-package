using System;
using System.Collections;
using System.Collections.Generic;

namespace Simplic.Package
{
    public class ObjectListItem
    {
        public string Source { get; set; }
        public string Target { get; set; }
        public Guid? Guid { get; set; }
        public InstallMode Mode { get; set; }
        public IList<Payload> Payload { get; set; } = new List<Payload>();
    }
}