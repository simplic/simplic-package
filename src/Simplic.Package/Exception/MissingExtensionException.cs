using System;

namespace Simplic.Package
{
    /// <summary>
    /// A Exception thrown when an extension is not contained in the loaded extensions.
    /// Can be thrown when unpacking.
    /// </summary>
    public class MissingExtensionException : Exception
    {
        /// <summary>
        /// Initializes a new Simplic.Package.MissingExtensionException insance.
        /// </summary>
        public MissingExtensionException()
        {
        }

        /// <summary>
        /// Initializes a new Simplic.Package.MissingExtensionException insance.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public MissingExtensionException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new Simplic.Package.MissingExtensionException insance.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="inner">The exception that caused this exception, or null.</param>
        public MissingExtensionException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}