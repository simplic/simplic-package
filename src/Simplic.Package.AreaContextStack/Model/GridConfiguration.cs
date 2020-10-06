using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package.StackContextArea
{
    class GridConfiguration : IStackContextAreaConfiguration
    {
        public string Grid { get; set; }
        public bool StackBased { get; set; }
        public bool ConnectWithArchive { get; set; }
        public IList<ContextOfStackItem> ContextOfStack { get; set; }
    }
}
