using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package
{
    public class ValidateObjectResult
    {
        public bool IsOkay { get; set; }
        public string ErrorMessage { get; set; }
        public Exception Exception { get; set; }
    }
}
