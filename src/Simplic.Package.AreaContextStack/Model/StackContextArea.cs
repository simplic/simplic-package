using System;

namespace Simplic.Package.StackContextArea
{
    public class StackContextArea : IContent
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        public string Type { get; set; }
        public IStackContextAreaConfiguration Configuration { get; set; }
    }
}