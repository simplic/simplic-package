using iAnywhere.Data.SQLAnywhere;
using NDesk.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Simplic.Framework.DAL;
using Simplic.Package.Data.DB;
using Simplic.Package.Service;
using Simplic.Package.Sql;
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
            var initialize = false;

            var install = false;
            var path = "";
            var connectionString = "dbn=PackageTest;server=PackageTest;uid=admin;pwd=admin";

            var optionSet = new OptionSet()
            {
                { "m|mkconfig=", "Creates a package.json file with given Name.", v =>  {create = true; name = v; } },
                { "p|pack", "Creats a package archive from the package.json in the  current working directory.", v => pack = true },
                { "init|initialize", "Initializes the PackageSystem by adding needed tables to database", v => initialize = true },
                { "i|install=", "Installs a package from the given path.", v => {install = true; path = v; } },
                { "c|connection=", "The database connection string.", v =>  {connectionString = v; } },
                { "h|help",  "Shows help", v => showHelp = true },
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
            #region Register types
            var container = new UnityContainer();
            container.RegisterType<ILogService, LogService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IPackService, PackService>();
            container.RegisterType<IUnpackService, UnpackService>();
            container.RegisterType<IInstallService, InstallService>();
            container.RegisterType<IFileService, FileService>();
            container.RegisterType<ICheckDependencyService, CheckDependencyService>();
            container.RegisterType<IPackageTrackingRepository, PackageTrackingRepository>();
            container.RegisterType<IInitializePackageSystemService, InitializePackageSystemService>();
            container.RegisterType<IInitializePackageSystemRepository, InitializePackageSystemRepository>();
            container.RegisterType<IValidatePackageConfigurationService, ValidatePackageConfigurationService>();

            container.RegisterType<IInstallObjectService, InstallSqlService>("application");
            container.RegisterType<IObjectRepository, SqlRepository>("application");
            container.RegisterType<IPackObjectService, PackSqlService>("application");
            container.RegisterType<IUnpackObjectService, UnpackSqlService>("application");

            container.RegisterType<IInstallObjectService, InstallSqlService>("comboBox");
            container.RegisterType<IObjectRepository, SqlRepository>("comboBox");
            container.RegisterType<IPackObjectService, PackSqlService>("comboBox");
            container.RegisterType<IUnpackObjectService, UnpackSqlService>("comboBox");

            container.RegisterType<IInstallObjectService, InstallSqlService>("EplReport");
            container.RegisterType<IObjectRepository, SqlRepository>("EplReport");
            container.RegisterType<IPackObjectService, PackSqlService>("EplReport");
            container.RegisterType<IUnpackObjectService, UnpackSqlService>("EplReport");

            container.RegisterType<IInstallObjectService, InstallSqlService>("EplReportDesign");
            container.RegisterType<IObjectRepository, SqlRepository>("EplReportDesign");
            container.RegisterType<IPackObjectService, PackSqlService>("EplReportDesign");
            container.RegisterType<IUnpackObjectService, UnpackSqlService>("EplReportDesign");

            container.RegisterType<IInstallObjectService, InstallSqlService>("FormatList");
            container.RegisterType<IObjectRepository, SqlRepository>("FormatList");
            container.RegisterType<IPackObjectService, PackSqlService>("FormatList");
            container.RegisterType<IUnpackObjectService, UnpackSqlService>("FormatList");

            container.RegisterType<IInstallObjectService, InstallSqlService>("grid");
            container.RegisterType<IObjectRepository, SqlRepository>("grid");
            container.RegisterType<IPackObjectService, PackSqlService>("grid");
            container.RegisterType<IUnpackObjectService, UnpackSqlService>("grid");

            container.RegisterType<IInstallObjectService, InstallSqlService>("itemBox");
            container.RegisterType<IObjectRepository, SqlRepository>("itemBox");
            container.RegisterType<IPackObjectService, PackSqlService>("itemBox");
            container.RegisterType<IUnpackObjectService, UnpackSqlService>("itemBox");

            container.RegisterType<IInstallObjectService, InstallSqlService>("report");
            container.RegisterType<IObjectRepository, SqlRepository>("report");
            container.RegisterType<IPackObjectService, PackSqlService>("report");
            container.RegisterType<IUnpackObjectService, UnpackSqlService>("report");

            container.RegisterType<IInstallObjectService, InstallSqlService>("repository");
            container.RegisterType<IObjectRepository, SqlRepository>("repository");
            container.RegisterType<IPackObjectService, PackSqlService>("repository");
            container.RegisterType<IUnpackObjectService, UnpackSqlService>("repository");

            container.RegisterType<IInstallObjectService, InstallSqlService>("role");
            container.RegisterType<IObjectRepository, SqlRepository>("role");
            container.RegisterType<IPackObjectService, PackSqlService>("role");
            container.RegisterType<IUnpackObjectService, UnpackSqlService>("role");

            container.RegisterType<IInstallObjectService, InstallSqlService>("sequence");
            container.RegisterType<IObjectRepository, SqlRepository>("sequence");
            container.RegisterType<IPackObjectService, PackSqlService>("sequence");
            container.RegisterType<IUnpackObjectService, UnpackSqlService>("sequence");

            container.RegisterType<IInstallObjectService, InstallSqlService>("sql");
            container.RegisterType<IObjectRepository, SqlRepository>("sql");
            container.RegisterType<IPackObjectService, PackSqlService>("sql");
            container.RegisterType<IUnpackObjectService, UnpackSqlService>("sql");

            container.RegisterType<IInstallObjectService, InstallSqlService>("stack");
            container.RegisterType<IObjectRepository, SqlRepository>("stack");
            container.RegisterType<IPackObjectService, PackSqlService>("stack");
            container.RegisterType<IUnpackObjectService, UnpackSqlService>("stack");

            container.RegisterType<IInstallObjectService, InstallSqlService>("stackAutoconnector");
            container.RegisterType<IObjectRepository, SqlRepository>("stackAutoconnector");
            container.RegisterType<IPackObjectService, PackSqlService>("stackAutoconnector");
            container.RegisterType<IUnpackObjectService, UnpackSqlService>("stackAutoconnector");

            container.RegisterType<IInstallObjectService, InstallSqlService>("stackContextArea");
            container.RegisterType<IObjectRepository, SqlRepository>("stackContextArea");
            container.RegisterType<IPackObjectService, PackSqlService>("stackContextArea");
            container.RegisterType<IUnpackObjectService, UnpackSqlService>("stackContextArea");

            container.RegisterType<IInstallObjectService, InstallSqlService>("stackFulltext");
            container.RegisterType<IObjectRepository, SqlRepository>("stackFulltext");
            container.RegisterType<IPackObjectService, PackSqlService>("stackFulltext");
            container.RegisterType<IUnpackObjectService, UnpackSqlService>("stackFulltext");

            container.RegisterType<IInstallObjectService, InstallSqlService>("stackRegister");
            container.RegisterType<IObjectRepository, SqlRepository>("stackRegister");
            container.RegisterType<IPackObjectService, PackSqlService>("stackRegister");
            container.RegisterType<IUnpackObjectService, UnpackSqlService>("stackRegister");

            container.RegisterType<ISqlService, SqlService>();
            container.RegisterType<ISqlColumnService, SqlColumnService>();
            Framework.DAL.DALManager.Init(connectionString);
            Framework.DAL.ConnectionManager.Init(Thread.CurrentThread);
            #endregion


            var logService = container.Resolve<ILogService>();

            //WriteLogMessage;
            logService.MessageAdded += (o, e) =>
            {
                if ((int)e.LogLevel <= (int)verbosity)
                    MyWriteLine($"[{DateTime.Now.ToString("HH:mm:ss")}]{e.LogLevel}: [{e.Message}]", e.LogLevel);
            };


            if (create)
            {
                var packageConfiguration = new PackageConfiguration
                {
                    PackageFormatVersion = Version.Parse(PackageFormatVersion.Version),
                    Guid = new Guid(),
                    Name = name,
                    Version = new Version(1, 0, 0, 0),
                    Dependencies = new List<Dependency>(),
                    Objects = new Dictionary<string, IList<ObjectListItem>>()
                };

                var jsonSerializerSettings = new JsonSerializerSettings
                {
                    Converters = new List<JsonConverter> {
                        new VersionConverter()
                    }
                };

                var json = JsonConvert.SerializeObject(packageConfiguration, jsonSerializerSettings);
                File.WriteAllText("package.json", json);
                Console.WriteLine($"Succesfully created a new package.json!");
            }

            if (pack)
            {
                if (!File.Exists("package.json"))
                {
                    Console.WriteLine($"Couldent find a package.json file in {Path.GetFullPath(".")}");
                    return 1;
                }

                var fullPath = Path.GetFullPath($"./package.json");
                var packService = container.Resolve<IPackService>();
                await packService.Pack(File.ReadAllText(fullPath));
                Console.WriteLine($"Succesfully packed {fullPath}!");
            }

            if (initialize)
            {
                Console.WriteLine("Check database connection ...");
                using (var connection = ConnectionManager.GetOpenPoolConnection<SAConnection>(connectionString))
                {
                    Console.WriteLine("Connected!"); // TODO: Does this fail if conn string is wrong?
                }

                var initializePackageSystemService = container.Resolve<IInitializePackageSystemService>();
                await initializePackageSystemService.Initialize();
                return 0;
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
            return 1;
        }

        private void WriteLogMessage(object o, LogMessageEventArgs e)
        {
            //if ((int)e.LogLevel <= (int)verbosity)
             //   MyWriteLine($"[{DateTime.Now.ToString("HH:mm:ss")}]{e.LogLevel}: [{e.Message}]", e.LogLevel);
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