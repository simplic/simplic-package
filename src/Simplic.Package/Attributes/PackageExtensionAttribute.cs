using System;

namespace Simplic.Package
{
    /// <summary>
    /// Attribute to define the entry point of a package system extension.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class PackageExtensionAttribute : Attribute
    {
    }
}
