using NDesk.Options;
using Simplic.Package.Service;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Unity;

namespace Simplic.Package.CLI
{
    public class Program
    {
        public static async Task<int> Main(string[] args)
        {
            var showHelp = false;
            var pack = false;
            var install = false;
            var forceInstall = false;

            var dummy = ""; // Debug

            var path = "";

            var optionSet = new OptionSet()
            {
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
                ShowHelp(optionSet);
                return 0;
            }

            var container = new UnityContainer();
            container.RegisterType<IPackService, PackService>();
            container.RegisterType<IUnpackService, UnpackService>();
            container.RegisterType<IInstallService, InstallService>();

            // Debug
            if (dummy != "")
            {
                Console.WriteLine(dummy);

                if (forceInstall)
                {
                    Console.WriteLine("Force install");
                }
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
    }
}