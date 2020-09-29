using System;

namespace Simplic.Package
{
    public class CheckDependencyResult
    {
        public bool Exists { get; set; }
        public Version LatestExistingVersion { get; set; }
        public Dependency Dependency { get; set; }
    }
}