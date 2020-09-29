using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package
{
    public class MissingObjectException : Exception
    {
        public MissingObjectException() { }
        public MissingObjectException(string message) : base(message) { }
        public MissingObjectException(string message, Exception inner) : base(message, inner) { }

    }
}
