using NDesk.Options;
using Simplic.Package.Service;
using System;
using System.IO;
using System.Linq;
using Unity;

namespace Simplic.Package.CLI
{
    public class Program
    {
        public static int Main(string[] args)
        {
            var showHelp = false;
            var pack = false;
            var install = false;

            var path = "";

            var optionSet = new OptionSet()
            {
                { "p|pack:", "The path to the Package configuration file (package.json) directory. Defaults to the current working directory.", v => {pack = true; if (v == null) path = "."; else path = v; } },
                { "i|install=", "The path to the package.", v => {install = true; path = v; } },
                { "h|help",  "show this message and exit", v => showHelp = true }
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
                packService.Pack(fullPath);
            }

            if (install)
            {
                if (File.Exists(path))
                {
                    // Unpacking Package will throw InvalidPackageException if file isnt a package
                }
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