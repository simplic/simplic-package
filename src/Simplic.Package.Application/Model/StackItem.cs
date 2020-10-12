using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Simplic.Package.Application
{
    public class StackItem
    {
        public Guid Id { get; set; }
        public Guid StackId { get; set; }
        public bool IsVisible { get; set; }
        public string DisplayName { get; set; }
        public string Grid { get; set; }
        public int OrderId { get; set; }
        public IList<RegisterItem> Registers { get; set; }
        public string SearchName { get; set; } = "";
    }
}