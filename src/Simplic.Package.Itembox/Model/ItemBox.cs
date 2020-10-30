using System;
using System.Collections.Generic;

namespace Simplic.Package.ItemBox
{
    public class ItemBox : IContent
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IList<ItemBoxProfile> Profiles { get; set; } = new List<ItemBoxProfile>();
    }
}