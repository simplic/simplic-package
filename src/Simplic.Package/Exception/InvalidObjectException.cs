using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package
{
    class InvalidObjectException : Exception
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
