using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Simplic.Package.Service
{
    /// <summary>
    /// Helper class for extensions.
    /// </summary>
    public static class ExtensionHelper
    {
        private static string tempPath;

        /// <summary>
        /// Gets a list of all loaded extension names.
        /// </summary>
        public static IList<string> LoadedExtensions = new List<string>();

        /// <summary>
        /// Gets a temp path for extensions.
        /// <para>
        /// This will create a new path if none exists and will add it to the app domain.
        /// </para>
        /// </summary>
        /// <returns>The path</returns>
        public static string GetTempPath()
        {
            if (!string.IsNullOrWhiteSpace(tempPath))
                return tempPath;

            tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());

            Directory.CreateDirectory(tempPath);

            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

            return tempPath;
        }

        /// <summary>
        /// Adds the temp path to the assembly resolving path.
        /// For more information look into the Simplic.Main project.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            try
            {
                Assembly resolvedAssembly = null;
                string strTempAsmPath = string.Empty;

                Assembly objExecutingAssemblies = Assembly.GetExecutingAssembly();
                AssemblyName[] arrReferencedAssmbNames = objExecutingAssemblies.GetReferencedAssemblies();

                AssemblyName referencesAssemblyName = arrReferencedAssmbNames.Where(a => a.Name == args.Name)
                    .FirstOrDefault();

                // Assembly is already referenced
                if (referencesAssemblyName != null)
                {
                    try
                    {
                        resolvedAssembly = Assembly.LoadFrom(referencesAssemblyName.CodeBase);
                    }
                    catch
                    {
                        return null;
                    }
                }
                // Assembly is not referenced yet
                else
                {
                    string assemblyDllName = "";
                    if (args.Name.Contains(","))
                    {
                        assemblyDllName = args.Name.Substring(0, args.Name.IndexOf(",")) + ".dll";
                    }
                    else
                    {
                        assemblyDllName = args.Name + ".dll";
                    }

                    string completePath = Path.Combine(tempPath, assemblyDllName);

                    if (!string.IsNullOrEmpty(completePath))
                    {
                        if (File.Exists(completePath))
                        {
                            try
                            {
                                // Loads the assembly from the specified path.
                                resolvedAssembly = Assembly.LoadFrom(completePath);
                            }
                            catch
                            {
                                return null;
                            }
                        }
                    }

                }
                // Returns the loaded assembly.
                return resolvedAssembly;
            }
            catch
            {
                return null;
            }
        }

    }
}
