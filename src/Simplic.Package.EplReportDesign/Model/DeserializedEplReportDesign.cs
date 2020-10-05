using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package.EplReportDesign
{
    public class DeserializedEplReportDesign : IContent
    {
        public Guid Id { get; set; }
        public string InternalName { get; set; }
        public string DisplayName { get; set; }
        public string ReportContent { get; set; }
        public int PrinterHeadWidth { get; set; }
    }
}
