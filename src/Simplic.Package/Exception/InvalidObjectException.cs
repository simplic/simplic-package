using System;

namespace Simplic.Package
{
    /// <summary>
    /// Exception that is thrown when a object is invalid
    /// Can be thrown when packing (validation), unpacking or installing
    /// </summary>
    public class InvalidObjectException : Exception
    {
        public InvalidObjectException()
        {
        }

        public InvalidObjectException(string message) : base(message)
        {
        }

        public InvalidObjectException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}