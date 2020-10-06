using System.Collections;
using System.Collections.Generic;

namespace Simplic.Package
{
    public class PackObjectResult
    {
        public byte[] File { get; set; }
        public string Location { get; set; }
        public IDictionary<string, byte[]> Payload { get; set; }
    }
}