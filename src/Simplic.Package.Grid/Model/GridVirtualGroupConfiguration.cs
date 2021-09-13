using System;

namespace Simplic.Package.Grid
{
    /// <summary>
    /// Represents the content of a grid virtual group configuration.
    /// </summary>
    public class GridVirtualGroupConfiguration
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the expression text.
        /// </summary>
        public string ExpressionText { get; set; }

        /// <summary>
        /// Gets or sets the valid exression text.
        /// </summary>
        public string ValidExpressionText { get; set; }

        /// <summary>
        /// Gets or sets the group name.
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// Gets or sets the serialized expression text.
        /// </summary>
        public string SerializedExpressionText { get; set; }

        /// <summary>
        /// Gets or sets the is active flag.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the is valid flag.
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// Gets or sets the view data template.
        /// </summary>
        public string ViewDataTemplate { get; set; }

        /// <summary>
        /// Gets or sets the edit data template.
        /// </summary>
        public string EditDataTemplate { get; set; }

        /// <summary>
        /// Gets or sets the view data template path.
        /// </summary>
        public string ViewDataTemplatePath { get; set; }

        /// <summary>
        /// Gets or sets the edit data template path.
        /// </summary>
        public string EditDataTemplatePath { get; set; }

        /// <summary>
        /// Gets or sets the item filter text
        /// </summary>
        public string ItemFilterText { get; set; }
    }
}