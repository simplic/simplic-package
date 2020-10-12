using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package
{
    /// <summary>
    /// Exception that is thrown, when there is a mismatch between accepted content types of an object and the given content type
    /// Also thrown when there is no implementation for given content type
    /// </summary>
    public class InvalidContentException : Exception
    {
        public InvalidContentException()
        {
        }

        public InvalidContentException(string message) : base(message)
        {
        }

        public InvalidContentException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
