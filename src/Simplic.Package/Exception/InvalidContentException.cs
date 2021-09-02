using System;

namespace Simplic.Package
{
    /// <summary>
    /// Exception that is thrown, when there is a mismatch between accepted content types of an object and the given content type.
    /// Also thrown when there is no implementation for given content type.
    /// </summary>
    public class InvalidContentException : Exception
    {
        /// <summary>
        /// Initializes a new Simplic.Package.InvalidContentException instace.
        /// </summary>
        public InvalidContentException()
        {
        }

        /// <summary>
        /// Initializes a new Simplic.Package.InvalidContentException instace.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public InvalidContentException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new Simplic.Package.InvalidContentException instace.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="inner">The exception that caused this exception, or null.</param>
        public InvalidContentException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}