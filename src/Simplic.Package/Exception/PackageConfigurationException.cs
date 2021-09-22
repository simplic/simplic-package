using System;

namespace Simplic.Package
{
    /// <summary>
    /// An exception thrown when the package configuration is invalid.
    /// Can be thrown when packing or unpacking.
    /// </summary>
    public class PackageConfigurationException : Exception
    {
        /// <summary>
        /// Initializes a new Simplic.Package.PackageConfigurationException instance.
        /// </summary>
        public PackageConfigurationException()
        {
        }

        /// <summary>
        /// Initializes a new Simplic.Package.PackageConfigurationException instance.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public PackageConfigurationException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new Simplic.Package.PackageConfigurationException instance.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that caused this exception, or null.</param>
        public PackageConfigurationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}