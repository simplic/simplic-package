using Simplic.UI.GridView;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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