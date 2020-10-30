using System.Collections.Generic;

namespace Simplic.Package.FormatList
{
    public class FormatList : IContent
    {
        public string InternalName { get; set; }
        public string Description { get; set; }
        public IList<FormatListItem> Items { get; set; } = new List<FormatListItem>();
    }
}