using System;

namespace Simplic.Package
{
    /// <summary>
    /// A Exception thrown when a package is invalid
    /// Can be thrown when unpacking
    /// </summary>
    public class InvalidPackageException : Exception
    {
        public InvalidPackageException()
        {
        }

        public InvalidPackageException(string message) : base(message)
        {
        }

        public InvalidPackageException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}