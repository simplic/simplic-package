using System;

namespace Simplic.Package
{
    /// <summary>
    /// Exception that is thrown when there is either a later Version or the given Version of a Package is already installed.
    /// </summary>
    public class ExistingPackageException : Exception
    {
        /// <summary>
        /// Initializes a new instance of Simplic.Package.ExistingPackageException instance.
        /// </summary>
        public ExistingPackageException()
        {
        }

        /// <summary>
        /// Initializes a new instance of Simplic.Package.ExistingPackageException instance.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ExistingPackageException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of Simplic.Package.ExistingPackageException instance.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="inner">The exception that caused this exception, or null.</param>
        public ExistingPackageException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}