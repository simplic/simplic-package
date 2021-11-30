using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using Unity;

namespace Simplic.Package.Service
{
    /// <inheritdoc cref="IExtensionService"/>
    public class ExtensionService : IExtensionService
    {
        private readonly ILogService logService;
        private readonly IUnityContainer container;

        /// <summary>
        /// Initializes a new instance of <see cref="ExtensionService"/>.
        /// </summary>
        /// <param name="logService">Instance of an implementation of <see cref="ILogService"/>.</param>
        /// <param name="container">Instance of an implementation of <see cref="IUnityContainer"/>.</param>
        public ExtensionService(ILogService logService, IUnityContainer container)
        {
            this.logService = logService;
            this.container = container;
        }

        /// <inheritdoc/>
        public async void LoadExtensions(IList<string> extensions)
        {
            //load all assemblies in extension folder.

            foreach (var dll in Directory.GetFiles($"{Environment.CurrentDirectory}\\extension")
                .Where(x => x.ToLower().EndsWith(".dll")))
            {
                File.Copy(dll, $"{ExtensionHelper.GetTempPath()}\\{Path.GetFileName(dll)}");
            }

            foreach (var extension in extensions)
            {
                if (ExtensionHelper.LoadedExtensions.Contains(extension))
                {
                    await logService.WriteAsync($"Extension '{extension}' already contained in loaded extensions",
                        LogLevel.Debug);
                    continue;
                }

                Assembly assembly = null;

                try
                {
                    assembly = Assembly.LoadFile($"{Environment.CurrentDirectory}\\extension\\{extension}");
                    await logService.WriteAsync($"Assembly loaded from \\extension\\{extension}", LogLevel.Info);
                }
                catch
                {
                    await logService.WriteAsync($"Could not load extension at: " +
                        $"{Path.GetFullPath($"\\extension\\{extension}")}", LogLevel.Error);
                    continue;
                }


                if (assembly == null)
                    continue;

                LoadExtension(assembly, extension);
            }
        }

        /// <inheritdoc/>
        public async void LoadExtensionsFromZipArchive(IList<string> extensions, ZipArchive archive)
        {
            foreach (var dll in archive.Entries.Where(x =>
                x.FullName.StartsWith("extension\\") &&
                x.FullName.ToLower().EndsWith(".dll")))
            {
                using (var filestream = new FileStream($"{ExtensionHelper.GetTempPath()}\\{dll.Name}", FileMode.Create))
                {
                    dll.Open().CopyTo(filestream);
                    filestream.Close();
                }

            }

            foreach (var extension in extensions)
            {
                var dll = archive.Entries.FirstOrDefault(x => x.Name == extension);

                if (ExtensionHelper.LoadedExtensions.Contains(extension))
                {
                    await logService.WriteAsync($"Extension '{extension}' already contained in loaded extensions",
                        LogLevel.Debug);
                    continue;
                }

                Assembly assembly = null;

                try
                {
                    assembly = Assembly.Load(ReadStream(dll.Open()));
                    await logService.WriteAsync($"Assembly loaded from {extension}", LogLevel.Info);
                }
                catch
                {
                    await logService.WriteAsync($"Could not load extension at: " +
                        $"{extension}", LogLevel.Error);
                    continue;
                }

                if (assembly == null)
                    continue;

                LoadExtension(assembly, extension);
            }
        }

        /// <summary>
        /// Laods the extension from the assembly.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <param name="extensionName">name of the extension.</param>
        private async void LoadExtension(Assembly assembly, string extensionName)
        {
            await logService.WriteAsync($"Assembly found with full name: {assembly.FullName}", LogLevel.Debug);

            Type type = null;

            try
            {
                type = assembly.GetTypes()
                    .Where(x =>
                        x.GetCustomAttributes(typeof(PackageExtensionAttribute)).Any() &&
                        x.IsPublic)
                    .First();
            }
            catch
            {
                await logService.WriteAsync($"Could not find any public class with the attribute: " +
                    $"{nameof(PackageExtensionAttribute)} in {assembly.FullName}. \n" +
                    $"Make sure the initialization class has the attribute " +
                    $"and a public accessibility.", LogLevel.Error);
                return;
            }

            if (type == null)
                return;

            await logService.WriteAsync($"Type found with full name: {type.FullName}", LogLevel.Debug);

            MethodInfo method = null;

            try
            {
                method = type.GetMethods().First(x =>
                    x.Name == "Initialize" &&
                    x.IsStatic &&
                    x.GetParameters().First().ParameterType == typeof(IUnityContainer));

            }
            catch
            {
                await logService.WriteAsync($"Could not find a method named \"Initialize\" " +
                    $"with the first parameter being of type {nameof(IUnityContainer)} in {assembly.FullName}",
                    LogLevel.Error);
                return;
            }

            if (method == null)
                return;

            await logService.WriteAsync($"Method found with name: {method.Name}", LogLevel.Debug);

            try
            {
                method.Invoke(null, new object[] { container });
            }
            catch (Exception ex)
            {
                await logService.WriteAsync($"Could not execute mehtod {method.Name} in {type.FullName}",
                    LogLevel.Error, ex);
                return;
            }

            ExtensionHelper.LoadedExtensions.Add(extensionName);
            await logService.WriteAsync($"Succesfully loaded extension {extensionName}.", LogLevel.Info);
        }


        private static byte[] ReadStream(Stream stream)
        {
            using (var ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}
