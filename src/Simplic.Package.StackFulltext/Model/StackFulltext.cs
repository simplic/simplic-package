using System;

namespace Simplic.Package.StackFulltext
{
    public class StackFulltext : IContent
    {
        public Guid Id { get; set; }
        public Guid StackId { get; set; }
        public string Type { get; set; }
        public IStackFulltextConfiguration Configuration { get; set; }
    }
}