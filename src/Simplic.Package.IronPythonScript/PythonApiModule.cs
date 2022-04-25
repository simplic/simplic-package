using IronPython.Runtime;

[assembly: PythonModule("simplic_package", typeof(Simplic.Package.IronPythonScript.PythonApiModule))]
namespace Simplic.Package.IronPythonScript
{
    /// <summary>
    /// Simplic package api module base.
    /// </summary>
    public static partial class PythonApiModule
    {
        public const string __doc__ = "The python module is part of the simplic package python api.";
    }
}
