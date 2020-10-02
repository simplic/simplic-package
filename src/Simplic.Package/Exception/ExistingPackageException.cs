using System;

namespace Simplic.Package
{
    /// <summary>
    /// TODO: Is this needed?
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