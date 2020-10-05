using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package.Stack
{
    public class DeserializedStack : IContent
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        public string StackGridName { get; set; }
        public bool IsActive { get; set; }
        public bool ConnectWithArchive { get;set; }
        public string TableName { get; set; }
        public string StackName { get; set; }
        public string HeaderSql { get; set; }
        public bool TrackChanges { get; set; }
        public FullText FullText { get; set; }
    }
}
