namespace Simplic.Package
{
    /// <summary>
    /// Applicaiton relevant information for e.g. the application mode.
    /// </summary>
    public static class ApplicationSettings
    {
        /// <summary>
        /// Gets or sets the application mode.
        /// <para>
        /// This one should not be changed other than on startup.
        /// </para>
        /// </summary>
        public static ApplicationMode ApplicationMode { get; set; }
    }
}
