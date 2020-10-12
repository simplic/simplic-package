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
using Simplic.Package.ComboBox;
using Simplic.Package.ItemBox;
using Simplic.Package.EplReportDesign;
using Simplic.Package.EplReport;
using Simplic.Package.FormatList;
using Simplic.Package.Grid;
using Simplic.Package.Report;
using Simplic.Package.Repository;
using Simplic.Package.Role;
using Simplic.Package.Sequence;
using Simplic.Package.Stack;
using Simplic.Package.StackAutoconnector;
using Simplic.Package.StackContextArea;
using Simplic.Package.StackFulltext;
using Simplic.Package.StackRegister;
using Simplic.Package.Application;
using Simplic.Framework.Core;
using Unity.ServiceLocation;
using Simplic.Localization;
using Simplic.Log.Console;
using Simplic.Reporting;
using Simplic.Framework.Reporting;

namespace Simplic.Package.CLI
{
    public class Program
    {
        public static async Task<int> Main(string[] args)
        {
            var showHelp = false;
            var verbosity = LogLevel.Debug;

            var create = false;
            var name = "";

            var pack = false;
            var initialize = false;

            var install = false;
            var path = "";
            var connectionString = "dbn=PackageTest;server=PackageTest;uid=admin;pwd=school";

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
            container.RegisterType<IMigrationService, MigrationService>();
            container.RegisterType<IMigrationRepository, MigrationRepository>();

            container.RegisterType<IInstallObjectService, InstallApplicationService>("application");
            container.RegisterType<IObjectRepository, ApplicationRepository>("application");
            container.RegisterType<IPackObjectService, PackApplicationService>("application");
            container.RegisterType<IUnpackObjectService, UnpackApplicationService>("application");

            container.RegisterType<IInstallObjectService, InstallComboBoxService>("comboBox");
            container.RegisterType<IObjectRepository, ComboBoxRepository>("comboBox");
            container.RegisterType<IPackObjectService, PackComboBoxService>("comboBox");
            container.RegisterType<IUnpackObjectService, UnpackComboBoxService>("comboBox");

            container.RegisterType<IInstallObjectService, InstallEplReportService>("eplReport");
            container.RegisterType<IObjectRepository, EplReportRepository>("eplReport");
            container.RegisterType<IPackObjectService, PackEplReportService>("eplReport");
            container.RegisterType<IUnpackObjectService, UnpackEplReportService>("eplReport");

            container.RegisterType<IInstallObjectService, InstallEplReportDesignService>("eplReportDesign");
            container.RegisterType<IObjectRepository, EplReportDesignRepository>("eplReportDesign");
            container.RegisterType<IPackObjectService, PackEplReportDesignService>("eplReportDesign");
            container.RegisterType<IUnpackObjectService, UnpackEplReportDesignService>("eplReportDesign");

            container.RegisterType<IInstallObjectService, InstallFormatListService>("formatList");
            container.RegisterType<IObjectRepository, FormatListRepository>("formatList");
            container.RegisterType<IPackObjectService, PackFormatListService>("formatList");
            container.RegisterType<IUnpackObjectService, UnpackFormatListService>("formatList");

            container.RegisterType<IInstallObjectService, InstallGridService>("grid");
            // container.RegisterType<IObjectRepository, SqlRepository>("grid");
            container.RegisterType<IPackObjectService, PackGridService>("grid");
            container.RegisterType<IUnpackObjectService, UnpackGridService>("grid");

            container.RegisterType<IInstallObjectService, InstallItemBoxService>("itemBox");
            container.RegisterType<IObjectRepository, ItemBoxRepository>("itemBox");
            container.RegisterType<IPackObjectService, PackItemBoxService>("itemBox");
            container.RegisterType<IUnpackObjectService, UnpackItemBoxService>("itemBox");

            container.RegisterType<IInstallObjectService, InstallReportService>("report");
            container.RegisterType<IObjectRepository, ReportRepository>("report");
            container.RegisterType<IPackObjectService, PackReportService>("report");
            container.RegisterType<IUnpackObjectService, UnpackReportService>("report");

            container.RegisterType<IInstallObjectService, InstallRepositoryService>("repository");
            container.RegisterType<IObjectRepository, RepositoryRepository>("repository");
            container.RegisterType<IPackObjectService, PackRepositoryService>("repository");
            container.RegisterType<IUnpackObjectService, UnpackRepositoryService>("repository");

            container.RegisterType<IInstallObjectService, InstallRoleService>("role");
            container.RegisterType<IObjectRepository, RoleRepository>("role");
            container.RegisterType<IPackObjectService, PackRoleService>("role");
            container.RegisterType<IUnpackObjectService, UnpackRoleService>("role");

            container.RegisterType<IInstallObjectService, InstallSequenceService>("sequence");
            container.RegisterType<IObjectRepository, SequenceRepository>("sequence");
            container.RegisterType<IPackObjectService, PackSequenceService>("sequence");
            container.RegisterType<IUnpackObjectService, UnpackSequenceService>("sequence");

            container.RegisterType<IInstallObjectService, InstallSqlService>("sql");
            container.RegisterType<IObjectRepository, SqlRepository>("sql");
            container.RegisterType<IPackObjectService, PackSqlService>("sql");
            container.RegisterType<IUnpackObjectService, UnpackSqlService>("sql");

            container.RegisterType<IInstallObjectService, InstallStackService>("stack");
            container.RegisterType<IObjectRepository, StackRepository>("stack");
            container.RegisterType<IPackObjectService, PackStackService>("stack");
            container.RegisterType<IUnpackObjectService, UnpackStackService>("stack");

            container.RegisterType<IInstallObjectService, InstallStackAutoconnectorService>("stackAutoconnector");
            container.RegisterType<IObjectRepository, StackAutoconnectorRepository>("stackAutoconnector");
            container.RegisterType<IPackObjectService, PackStackAutoconnectorService>("stackAutoconnector");
            container.RegisterType<IUnpackObjectService, UnpackStackAutoconnectorService>("stackAutoconnector");

            container.RegisterType<IInstallObjectService, InstallStackContextAreaService>("stackContextArea");
            container.RegisterType<IObjectRepository, StackContextAreaRepository>("stackContextArea");
            container.RegisterType<IPackObjectService, PackStackContextAreaService>("stackContextArea");
            container.RegisterType<IUnpackObjectService, UnpackStackContextAreaService>("stackContextArea");

            container.RegisterType<IInstallObjectService, InstallStackFulltextService>("stackFulltext");
            container.RegisterType<IObjectRepository, StackFulltextRepository>("stackFulltext");
            container.RegisterType<IPackObjectService, PackStackFulltextService>("stackFulltext");
            container.RegisterType<IUnpackObjectService, UnpackStackFulltextService>("stackFulltext");

            container.RegisterType<IInstallObjectService, InstallStackRegisterService>("stackRegister");
            container.RegisterType<IObjectRepository, StackRegisterRepository>("stackRegister");
            container.RegisterType<IPackObjectService, PackStackRegisterService>("stackRegister");
            container.RegisterType<IUnpackObjectService, UnpackStackRegisterService>("stackRegister");

            container.RegisterType<ISqlService, SqlService>();
            container.RegisterType<ISqlColumnService, SqlColumnService>();
            Framework.DAL.DALManager.Init(connectionString);
            Framework.DAL.ConnectionManager.Init(Thread.CurrentThread);

            container.RegisterType<ILocalizationService, LocalizationService>();

            var serviceLocator = new UnityServiceLocator(container);
            CommonServiceLocator.ServiceLocator.SetLocatorProvider(() => serviceLocator);

            var singelton = ReportManager.Singleton;
            singelton.Initialize(new DefaultReportConfigurationProvider());

            Log.LogManagerInstance.Instance.OutputProvider.Add(new ConsoleLogOutput());
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
                    Console.WriteLine("Connected!");
                }

                var initializePackageSystemService = container.Resolve<IInitializePackageSystemService>();
                await initializePackageSystemService.Initialize();
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
            }
            return 0;
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