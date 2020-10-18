using System;

namespace Simplic.Package
{
    /// <summary>
    /// Exception that is thrown when there is either a later Version or the given Version of a Package is already installed
    /// </summary>
    public class ExistingPackageException : Exception
    {
        public ExistingPackageException()
        {
        }

        public ExistingPackageException(string message) : base(message)
        {
        }

        public ExistingPackageException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}