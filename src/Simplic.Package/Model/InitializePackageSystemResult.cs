using System;

namespace Simplic.Package
{
    public class InitializePackageSystemResult : LogResult
    {
        public bool CreatedTablePackage { get; set; } = false;
        public bool CreatedTablePackageObject { get; set; } = false;
    }
}