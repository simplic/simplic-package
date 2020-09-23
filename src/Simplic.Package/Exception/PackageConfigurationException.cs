using System;

namespace Simplic.Package
{
    public class PackageConfigurationException : Exception
    {
        public PackageConfigurationException()
        {
        }

        public PackageConfigurationException(string message) : base(message)
        {
        }

        public PackageConfigurationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
