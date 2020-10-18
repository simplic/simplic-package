using System;

namespace Simplic.Package.Grid
{
    public class GridVirtualGroupConfiguration
    {
        public Guid Id { get; set; }
        public string ExpressionText { get; set; }
        public string ValidExpressionText { get; set; }
        public string GroupName { get; set; }
        public string SerializedExpressionText { get; set; }
        public bool IsActive { get; set; }
        public bool IsValid { get; set; }
        public string ViewDataTemplate { get; set; }
        public string EditDataTemplate { get; set; }
        public string ViewDataTemplatePath { get; set; }
        public string EditDataTemplatePath { get; set; }
        public string ItemFilterText { get; set; }
    }
}