using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package
{
    public class MissingDependencyException : Exception
    {
        public MissingDependencyException() { }
        public MissingDependencyException(string message) : base(message) { }
    }
}
