using iAnywhere.Data.SQLAnywhere;
using NDesk.Options;
using Newtonsoft.Json;
using Simplic.Framework.DAL;
using Simplic.Package.Data.DB;
using Simplic.Package.Service;
using Simplic.Package.Service.Install;
using Simplic.Package.Service.Unpack;
using Simplic.Sql;
using Simplic.Sql.SqlAnywhere.Service;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Unity;
using Unity.Lifetime;
using Console = Colorful.Console;

namespace Simplic.Package.CLI
{
    public class Program
    {
        public static async Task<int> Main(string[] args)
        {
            var showHelp = false;
            var verbosity = LogLevel.Error;

            var create = false;
            var name = "";

            var pack = false;

            var install = false;
            var forceInstall = false; // TODO
            var path = "";
            var connectionString = "dbn=PackageTest;server=PackageTest;uid=admin;pwd=admin";

            var optionSet = new OptionSet()
            {
                { "m|mkconfig=", "Creates a package.json file with given Name.", v =>  {create = true; name = v; } },
                { "p|pack", "Creats a package archive from the package.json in the  current working directory.", v => pack = true },
                { "i|install=", "Installs a package from the given path.", v => {install = true; path = v; } },
                { "c|connection=", "The database connection string.", v =>  {connectionString = v; } },
                { "h|help",  "Shows help", v => showHelp = true },
                { "f|force", "Set to force the install. TODO: DOES NOTHING!", v => forceInstall = true },
                { "v|verbosity=", "Sets the Loglevel.\n Error: 0, Warning: 1, Info: 2, Debug: 3. Default: 0", v => verbosity = (LogLevel)Int32.Parse(v) },
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

            // Debugger.Launch();
            var container = new UnityContainer();
            container.RegisterType<ILogService, LogService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IPackService, PackService>();
            container.RegisterType<IUnpackService, UnpackService>();
            container.RegisterType<IInstallService, InstallService>();
            container.RegisterType<IFileService, FileService>();
            container.RegisterType<ICheckDependencyService, CheckDependencyService>();
            container.RegisterType<IPackageTrackingRepository, PackageTrackingRepository>();

            container.RegisterType<IInstallObjectService, InstallSqlService>("sql");
            container.RegisterType<IObjectRepository, SqlRepository>("sql");
            container.RegisterType<IPackObjectService, PackSqlService>("sql");
            container.RegisterType<IUnpackObjectService, UnpackSqlService>("sql");

            container.RegisterType<ISqlService, SqlService>();
            container.RegisterType<ISqlColumnService, SqlColumnService>();
            Framework.DAL.DALManager.Init(connectionString);
            Framework.DAL.ConnectionManager.Init(Thread.CurrentThread);

            var logService = container.Resolve<ILogService>();

            logService.MessageAdded += (sender, e) =>
            {
                if ((int)e.LogLevel <= (int)verbosity)
                    MyWriteLine(e.Message, e.LogLevel);
            };

            if (create)
            {
                var packageConfiguration = new PackageConfiguration
                {
                    PackageFormatVersion = new Version(1, 0, 0, 0),
                    Name = name,
                    Version = new Version(1, 0, 0, 0),
                    Dependencies = new List<Dependency>(),
                    Objects = new Dictionary<string, IList<ObjectListItem>>()
                };

                var json = JsonConvert.SerializeObject(packageConfiguration);
                File.WriteAllText("package.json", json);
                Console.WriteLine($"Succesfully created a new package.json!");
            }

            if (pack)
            {
                var packageConfig = Directory.GetFiles(".", "package.json", SearchOption.TopDirectoryOnly).FirstOrDefault();

                if (packageConfig == null)
                {
                    Console.WriteLine($"Couldent find a package.json file in {Path.GetFullPath(".")}");
                    return 1;
                }

                var fullPath = Path.GetFullPath($"./{packageConfig}");
                var packService = container.Resolve<IPackService>();
                await packService.Pack(File.ReadAllText(fullPath));
                Console.WriteLine($"Succesfully packed {fullPath}!");
            }

            if (install)
            {
                if (!File.Exists(path))
                {
                    Console.WriteLine($"File {Path.GetFullPath(path)} not found!");
                    return 1;
                }

                Console.WriteLine("Check database connection ...");
                using (var connection = ConnectionManager.GetOpenPoolConnection<SAConnection>(connectionString))
                {
                    Console.WriteLine("Connected!");
                }

                var unpackService = container.Resolve<IUnpackService>();
                var unpackedPackage = await unpackService.Unpack(Path.GetFullPath(path));

                var installService = container.Resolve<IInstallService>();

                // TODO: Force install
                await installService.Install(unpackedPackage);
                Console.WriteLine($"Succesfully installed {unpackedPackage.Name}!");
                return 0;
            }
            return 0;
        }

        private static void MyWriteLine(string message, LogLevel logLevel)
        {
            if (logLevel == LogLevel.Error)
                Console.WriteLine(message, Color.Red);
            else if (logLevel == LogLevel.Warning)
                Console.WriteLine(message, Color.Orange);
            else if (logLevel == LogLevel.Info)
                Console.WriteLine(message, Color.WhiteSmoke);
            else if (logLevel == LogLevel.Debug)
                Console.WriteLine(message, Color.Yellow);
            else
                Console.WriteLine(message);
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