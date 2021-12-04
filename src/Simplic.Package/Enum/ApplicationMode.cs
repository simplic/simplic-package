namespace Simplic.Package
{
    /// <summary>
    /// Represents the mode in which the application is opened.
    /// Should be relevant for the IRequestValueService.
    /// </summary>
    public enum ApplicationMode
    {
        /// <summary>
        /// The application is opened as CLI application.
        /// </summary>
        CLI = 0,

        /// <summary>
        /// The application is opened as UI application.
        /// </summary>
        UI = 1,

        /// <summary>
        /// The application is opened as in test mode.
        /// This should only be set in automated tests where no user interaction is possible.
        /// </summary>
        Test = 99
    }
}
