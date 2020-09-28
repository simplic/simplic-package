using System;

namespace Simplic.Package
{
    public class CheckDependencyResult
    {
        public bool Exists { get; set; }
        public Version ExistingVersion { get; set; }
    }
}