using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package
{
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
