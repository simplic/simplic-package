using System;

namespace Simplic.Package
{
    /// <summary>
    /// A Exception thrown when a object is missing from a package
    /// Can be thrown when unpacking
    /// </summary>
    public class MissingObjectException : Exception
    {
        public MissingObjectException()
        {
        }

        public MissingObjectException(string message) : base(message)
        {
        }

        public MissingObjectException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}