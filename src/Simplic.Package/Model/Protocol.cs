using System.Collections.Generic;

namespace Simplic.Package
{
    public class Protocol
    {
        public IList<string> Info { get; set; } = new List<string>();
        public IList<string> Warning { get; set; } = new List<string>();
        public IList<string> Error { get; set; } = new List<string>();
        public IList<string> Debug { get; set; } = new List<string>();
    }
}