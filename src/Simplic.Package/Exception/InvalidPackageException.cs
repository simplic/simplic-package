using System;

namespace Simplic.Package
{
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