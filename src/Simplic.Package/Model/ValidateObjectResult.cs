using System;

namespace Simplic.Package
{
    public class ValidateObjectResult : LogResult
    {
        public bool IsOkay { get; set; }
        public Exception Exception { get; set; }
    }
}