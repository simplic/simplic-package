﻿using Simplic.Package.Attributes;
using System;
using System.IO;
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


        public ExtensionService(ILogService logService, IUnityContainer container)
        {
            this.logService = logService;
            this.container = container;
        }


        /// <inheritdoc/>
        public async void LoadExtensions(Package package)
        {
            foreach (var extension in package.Extensions)
            {
                Assembly assembly = null;

                try
                {
                    assembly = Assembly.LoadFile($"\\extension\\{extension}");
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
                    continue;
                }

                if (type == null)
                    continue;

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
                    continue;
                }

                if (method == null)
                    continue;

                await logService.WriteAsync($"Method found with name: {method.Name}", LogLevel.Debug);

                try
                {
                    method.Invoke(null, new object[] { container });
                }
                catch (Exception ex)
                {
                    await logService.WriteAsync($"Could not execute mehtod {method.Name} in {type.FullName}",
                        LogLevel.Error, ex);
                    continue;
                }

                await logService.WriteAsync($"Succesfully loaded extension {extension}.", LogLevel.Info);
            }
        }
    }
}