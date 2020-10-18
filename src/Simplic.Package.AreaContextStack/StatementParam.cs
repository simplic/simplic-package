using System;

namespace Simplic.Package.StackContextArea
{
    internal class StatementParam
    {
        public Guid Id { get; set; }
        public Guid StackId { get; set; }
        public string DisplayName { get; set; }
        public string GridName {get;set;}
        public bool StackBased { get; set; }
        public bool ConnectWithArchive { get; set; }
        public string SearchName { get; set; }
    }
}