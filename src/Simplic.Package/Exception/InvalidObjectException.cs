using System;

namespace Simplic.Package
{
    /// <summary>
    /// Exception that is thrown when a object is invalid.
    /// Can be thrown when packing (validation), unpacking or installing.
    /// </summary>
    public class InvalidObjectException : Exception
    {
        /// <summary>
        /// Initializes a new Simplic.Package.InvalidObjectException instance.
        /// </summary>
        public InvalidObjectException()
        {
        }

        /// <summary>
        /// Initializes a new Simplic.Package.InvalidObjectException instance.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public InvalidObjectException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new Simplic.Package.InvalidObjectException instance.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that caused this exception, or null.</param>
        public InvalidObjectException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}