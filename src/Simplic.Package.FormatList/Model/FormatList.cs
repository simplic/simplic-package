using System.Collections.Generic;

namespace Simplic.Package.FormatList
{
    /// <summary>
    /// Represents the package content of a format list.
    /// </summary>
    public class FormatList : IContent
    {
        /// <summary>
        /// Gets or sets the internal name.
        /// </summary>
        public string InternalName { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        public IList<FormatListItem> Items { get; set; } = new List<FormatListItem>();
    }
}