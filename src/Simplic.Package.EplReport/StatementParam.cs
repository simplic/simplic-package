using System;

namespace Simplic.Package.EplReport
{
    /// <summary>
    /// Wraps some data to use as a parameter in sql statements.
    /// </summary>
    internal class StatementParam
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the internal name.
        /// </summary>
        public string InternalName { get; set; }

        /// <summary>
        /// Gets or sets the report design id.
        /// </summary>
        public string ReportDesignId { get; set; }

        /// <summary>
        /// Gets or sets the printer.
        /// </summary>
        public string Printer { get; set; }
        
        /// <summary>
        /// Gets or sets the IsContextlessPrintable flag.
        /// </summary>
        public bool IsContextlessPrintable { get; set; }

        /// <summary>
        /// Gets or sets the sql data source code.
        /// </summary>
        public string SqlDataSourceCode { get; set; }

        /// <summary>
        /// Gets or sets the sequence id.
        /// </summary>
        public Guid? SequenceId { get; set; }
    }
}
