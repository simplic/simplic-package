namespace Simplic.Package
{
    /// <summary>
    /// Represents the result of a mehtod which validates a package object
    /// <para>
    /// Contains additional data regarding the validation of the object.
    /// </para>
    /// </summary>
    public class ValidateObjectResult : LogResult
    {
        /// <summary>
        /// Gets or sets wherther the package object is valid.
        /// </summary>
        public bool IsValid { get; set; }
    }
}