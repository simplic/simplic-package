using System;

namespace Simplic.Package.EplReport
{
    internal class StatementHelper
    {
        public Guid Id { get; set; }
        public string InternalName { get; set; }
        public string ReportDesignId { get; set; }
        public string Printer { get; set; }
        public bool IsContextlessPrintable { get; set; }
        public string SqlDataSourceCode { get; set; }
        public Guid? SequenceId { get; set; }
    }
}
