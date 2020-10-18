using System;

namespace Simplic.Package
{
    /// <summary>
    /// An exception thrown when the package configuration is invalid
    /// Can be thrown when packing or unpacking
    /// </summary>
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