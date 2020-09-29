using NDesk.Options;
using Newtonsoft.Json;
using Simplic.Package.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Unity;

namespace Simplic.Package.CLI
{
    public class Program
    {
        public static async Task<int> Main(string[] args)
        {
            var showHelp = false;

            var create = false;
            var name = "";

            var pack = false;
            var install = false;
            var forceInstall = false;

            var dummy = ""; // Debug

            var path = "";

            var optionSet = new OptionSet()
            {
                {"c|create=", "The Name of the package you want to create.", v =>  {create = true; name = v; } },
                { "p|pack:", "The path to the Package configuration file (package.json) directory. Defaults to the current working directory.", v => {pack = true; if (v == null) path = "."; else path = v; } },
                { "i|install=", "The path to the package.", v => {install = true; path = v; } },
                { "h|help",  "show this message and exit", v => showHelp = true },
                { "f|force", "Set to force the install", v => forceInstall = true },
                { "d|dummy=", "", v => dummy = v } // Debug
            };

            if (!args.Any())
            {
                ShowHelp(optionSet);
                return 0;
            }

            try
            {
                optionSet.Parse(args);
            }
            catch (OptionException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Try `sipack --help' for more information.");
                return 1;
            }

            if (showHelp)
            {
                if (create)
                {
                    ShowConfigurationHelp();
                    return 0;
                }

                ShowHelp(optionSet);
                return 0;
            }

            var container = new UnityContainer();
            container.RegisterType<IPackService, PackService>();
            container.RegisterType<IUnpackService, UnpackService>();
            container.RegisterType<IInstallService, InstallService>();
            container.RegisterType<IFileService, FileService>();

            // Debug
            if (dummy != "")
            {
                Console.WriteLine(dummy);

                if (forceInstall)
                {
                    Console.WriteLine("Force install");
                }
            }

            if (create)
            {
                var packageConfiguration = new PackageConfiguration
                {
                    Name = name,
                    Version = new Version(1, 0, 0, 0),
                    Dependencies = new List<Dependency>(),
                    Objects = new Dictionary<string, IList<ObjectListItem>>()
                };

                var json = JsonConvert.SerializeObject(packageConfiguration);
                Console.WriteLine(json);
                File.WriteAllText("package.json", json);
            }

            if (pack)
            {
                if (!Directory.Exists(path))
                {
                    Console.WriteLine($"The path {Path.GetFullPath(path)} doesnt exist.");
                    return -1;
                }

                var packageConfig = Directory.GetFiles(path, "package.json", SearchOption.TopDirectoryOnly).FirstOrDefault();

                if (packageConfig == null)
                {
                    Console.WriteLine($"Couldent find a package.json file in {Path.GetFullPath(path)}");
                    return 1;
                }

                var fullPath = Path.GetFullPath($"{path}/{packageConfig}");
                Console.WriteLine(fullPath);

                var packService = container.Resolve<IPackService>();
                await packService.Pack(fullPath);
            }

            if (install)
            {
                if (!File.Exists(path))
                {
                    Console.WriteLine($"File {Path.GetFullPath(path)} not found!");
                    return 1;
                }

                // Unpacking Package will throw InvalidPackageException if file isnt a package
                var unpackService = container.Resolve<IUnpackService>();
                var unpackedPackage = await unpackService.Unpack(Path.GetFullPath(path));

                var installService = container.Resolve<IInstallService>();

                if (forceInstall) // Disregard missing dependencies, etc.
                {
                    await installService.Overwrite(unpackedPackage);
                }
                else
                { 
                    await installService.Install(unpackedPackage);
                }
                return 0;
            }
            return 0;
        }

        private static void ShowHelp(OptionSet optionSet)
        {
            Console.WriteLine("Usage: sipack [Options]");
            Console.WriteLine();
            Console.WriteLine("Options:");
            optionSet.WriteOptionDescriptions(Console.Out);
        }

        private static void ShowConfigurationHelp()
        {
            Console.WriteLine("Configuration Help");
        }
    }
}