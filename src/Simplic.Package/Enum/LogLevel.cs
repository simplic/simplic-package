namespace Simplic.Package
{
    /// <summary>
    /// Represents the log level.
    /// <para>
    /// The log level will indicate the severity of a log entry.
    /// </para>
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// This log level indicates that an error occured.
        /// </summary>
        Error = 0,

        /// <summary>
        /// Thid log level indicates a warning to the user.
        /// </summary>
        Warning = 1,

        /// <summary>
        /// This log level indicates a simple information.
        /// </summary>
        Info = 2,

        /// <summary>
        /// This log level indicates that the information is just for debug purposes.
        /// </summary>
        Debug = 3
    }
}