using System;

namespace Simplic.Package
{
    /// <summary>
    /// A Exception thrown when dependencies are missing
    /// Can be thrown when installing
    /// </summary>
    public class MissingDependencyException : Exception
    {
        public MissingDependencyException()
        {
        }

        public MissingDependencyException(string message) : base(message)
        {
        }

        public MissingDependencyException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}