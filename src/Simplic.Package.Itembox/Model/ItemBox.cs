using System.Collections.Generic;

namespace Simplic.Package.ItemBox
{
    /// <summary>
    /// Represents the content of an itembox.
    /// </summary>
    public class ItemBox : IContent
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the profiles.
        /// </summary>
        public IList<ItemBoxProfile> Profiles { get; set; } = new List<ItemBoxProfile>();
    }
}