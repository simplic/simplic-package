using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package
{
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
