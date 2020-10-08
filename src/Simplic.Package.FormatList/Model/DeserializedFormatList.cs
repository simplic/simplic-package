using System.Collections.Generic;

namespace Simplic.Package.FormatList
{
    public class DeserializedFormatList : IContent
    {
        public string InternalName { get; set; }
        public string Description { get; set; }
        public IList<FormatListItem> Items { get; set; }
    }
}