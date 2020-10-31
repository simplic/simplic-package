using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package.Ribbon
{
    /// <summary>
    /// Ribbon group configuration
    /// </summary>
    public class RibbonGroup
    {
        /// <summary>
        /// Gets or sets the ribbon id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the group name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the group order id
        /// </summary>
        public int OrderId { get; set; }
    }
}
