using Simplic.Dlr;
using System;
using System.IO;

namespace Simplic.Package.IronPythonScript
{
    /// <summary>
    /// Dlr host for the package system.
    /// </summary>
    public static class PackagePythonDlrHost
    {
        /// <summary>
        /// Initializes the dlr host.
        /// </summary>
        static PackagePythonDlrHost()
        {
            Initialize();
        }

        /// <summary>
        /// Initialize the language and the host.
        /// </summary>
        private static void Initialize()
        {
            Language = new IronPythonLanguage();
            Host = new DlrHost<IronPythonLanguage>(Language);

            var studioPath = Environment.GetEnvironmentVariable("Simplic Studio");
            var path = Path.Combine(studioPath, "Scripts", "Python");

            Host.AddSearchPath(path);
        }

        /// <summary>
        /// Reset script scope. Should only be used if you really know what it does.
        /// It creates a new scope and make the old one not available any longer.
        /// </summary>
        public static void ResetScriptScope()
        {
            Initialize();
        }

        /// <summary>
        /// Accessing host context
        /// </summary>
        public static DlrHost<IronPythonLanguage> Host { get; private set; }

        /// <summary>
        /// Accessing language context
        /// </summary>
        public static IronPythonLanguage Language { get; private set; }
    }
}
