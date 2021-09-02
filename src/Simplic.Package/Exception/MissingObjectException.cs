using System;

namespace Simplic.Package
{
    /// <summary>
    /// A Exception thrown when a object is missing from a package.
    /// Can be thrown when unpacking.
    /// </summary>
    public class MissingObjectException : Exception
    {
        /// <summary>
        /// Initializes a new Simplic.Package.MissingObjectException insance.
        /// </summary>
        public MissingObjectException()
        {
        }

        /// <summary>
        /// Initializes a new Simplic.Package.MissingObjectException insance.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public MissingObjectException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new Simplic.Package.MissingObjectException insance.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="inner">The exception that caused this exception, or null.</param>
        public MissingObjectException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}