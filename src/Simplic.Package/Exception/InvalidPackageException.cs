using System;

namespace Simplic.Package
{
    /// <summary>
    /// A Exception thrown when a package is invalid.
    /// Can be thrown when unpacking.
    /// </summary>
    public class InvalidPackageException : Exception
    {
        /// <summary>
        /// Initializes a new Simplic.Package.InvalidPackageException instance.
        /// </summary>
        public InvalidPackageException()
        {
        }

        /// <summary>
        /// Initializes a new Simplic.Package.InvalidPackageException instance.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public InvalidPackageException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new Simplic.Package.InvalidPackageException instance.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that caused this exception, or null.</param>
        public InvalidPackageException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}