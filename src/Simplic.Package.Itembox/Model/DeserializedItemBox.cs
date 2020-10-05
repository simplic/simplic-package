using System;
using System.Collections.Generic;

namespace Simplic.Package.ItemBox
{
    public class DeserializedItemBox : IContent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IList<ItemBoxProfile> Profiles { get; set; }
    }
}