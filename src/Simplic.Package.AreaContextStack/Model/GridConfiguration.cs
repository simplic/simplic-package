using System.Collections.Generic;

namespace Simplic.Package.StackContextArea
{
    internal class GridConfiguration : IStackContextAreaConfiguration
    {
        public string Grid { get; set; }
        public bool StackBased { get; set; }
        public bool ConnectWithArchive { get; set; }
    }
}