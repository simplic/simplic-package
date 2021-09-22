using System;

namespace Simplic.Package
{
    /// <summary>
    /// A Exception thrown when dependencies are missing.
    /// Can be thrown when installing.
    /// </summary>
    public class MissingDependencyException : Exception
    {
        /// <summary>
        /// Initializes a new Simplic.Package.MissingDependencyException instance.
        /// </summary>
        public MissingDependencyException()
        {
        }

        /// <summary>
        /// Initializes a new Simplic.Package.MissingDependencyException instance.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public MissingDependencyException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new Simplic.Package.MissingDependencyException instance.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="inner">The exception that caused this exception, or null.</param>
        public MissingDependencyException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}