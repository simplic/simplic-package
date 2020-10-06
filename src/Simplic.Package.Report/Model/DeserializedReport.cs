using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Simplic.Package.Report
{
    public class DeserializedReport : Simplic.Reporting.IReportConfiguration
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string ReportFile { get; set; }
        public IReportConfiguration Configuration { get; set; }
        public string Description { get; set; }
        public string ReportName { get; set; }
        public string NonePrivateReportName { get; set; }
        public Stream Report { get; set; }
        public bool IsPrivatized { get; set; }
        public bool IsContextless { get; set; }
        public Visibility ContextlessVisible { get; set; }
        public int? CheckoutUser { get; set; }
        public string PrinterName { get; set; }
        public string ProviderName { get; set; }
        public Simplic.Reporting.ReportType Type { get; set; }
    }
}
