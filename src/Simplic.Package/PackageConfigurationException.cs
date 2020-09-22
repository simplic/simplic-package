using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
