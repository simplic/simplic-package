using Newtonsoft.Json;
using System;

namespace Simplic.Package.Stack
{
    /// <summary>
    /// Gets or sets the stack content.
    /// </summary>
    public class Stack : IContent
    {
        /// <summary>
        /// Gets or sets the id.
        /// <para>
        /// Represents the unique identifier and primary key of a stack.
        /// </para>
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the stack grid name.
        /// </summary>
        public string StackGridName { get; set; }

        /// <summary>
        /// Gets or sets the is active flag.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the connect with archive flag.
        /// </summary>
        public bool ConnectWithArchive { get; set; }

        /// <summary>
        /// Gets or sets the table name.
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// Gets or sets the stack name.
        /// </summary>
        public string StackName { get; set; }

        /// <summary>
        /// Gets or sets the header sql.
        /// </summary>
        public string HeaderSql { get; set; }

        /// <summary>
        /// Gets or sets the track changes flag.
        /// </summary>
        public bool TrackChanges { get; set; }

        /// <summary>
        /// Gets or sets the fulltext content.
        /// </summary>
        public FullText FullText { get; set; }
    }
}