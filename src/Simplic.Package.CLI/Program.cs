using Sap.Data.SQLAnywhere;
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
using Simplic.Localization;
using Simplic.Log.Console;
using Simplic.Reporting;
using Simplic.Framework.Reporting;
using Simplic.Package.Icon;
using Simplic.Tracking;
using Simplic.TenantSystem;
using Simplic.Authorization;
using Simplic.Icon;
using Simplic.Icon.Service;
using Simplic.Package.Ribbon;
using Unity.ServiceLocation;
using Simplic.Package.Configuration;
using System.Diagnostics;
using Simplic.Package.IronPythonScript;

namespace Simplic.Package.CLI
{
    public class Program
    {
        public static async Task<int> Main(string[] args)
        {
            Debugger.Launch();

            var showHelp = false;
            var verbosity = LogLevel.Debug;

            var create = false;
            var name = "";

            var pack = false;
            var target = "";

            var initialize = false;

            var install = false;
            var path = "";
            var connectionString = "dbn=PackageTest;server=PackageTest;uid=admin;pwd=school";

            var testMode = false;

            var optionSet = new OptionSet()
            {
                { "m|mkconfig=", "Creates a package.json file with given Name.", v =>  {create = true; name = v; } },
                { "p|pack:", "Creats a package archive from the package.json in the current working directory.",
                    v => { target = v ?? ""; pack = true; } },
                { "init|initialize", "Initializes the PackageSystem by adding needed tables to database",
                    v => initialize = true },
                { "i|install=", "Installs a package from the given path.", v => {install = true; path = v; } },
                { "c|connection=", "The database connection string.", v =>  {connectionString = v; } },
                { "h|help",  "Shows help", v => showHelp = true },
                { "v|verbosity=", "Sets the Loglevel.\n Error: 0, Warning: 1, Info: 2, Debug: 3. Default: 0",
                    v => verbosity = (LogLevel)Int32.Parse(v) },
                { "test",
                    "Starts the application in test mode, this should just be set in automated tests." +
                    " This will not test wether the package is installable, this will disable some features " +
                    "that require user intput for testing purposes", v => testMode = true }
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
                Console.WriteLine("Try `simpack --help' for more information.");
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

            if (testMode)
                ApplicationSettings.ApplicationMode = ApplicationMode.Test;
            else
                ApplicationSettings.ApplicationMode = ApplicationMode.CLI;

            #region Register types
            var container = new UnityContainer();
            container.RegisterType<ILogService, LogService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IExtensionService, ExtensionService>();
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

            container.RegisterType<IInstallObjectService, InstallRibbonService>("ribbon");
            container.RegisterType<IObjectRepository, RibbonRepository>("ribbon");
            container.RegisterType<IPackObjectService, PackRibbonService>("ribbon");
            container.RegisterType<IUnpackObjectService, UnpackRibbonService>("ribbon");

            container.RegisterType<IInstallObjectService, InstallComboBoxService>("comboBox");
            container.RegisterType<IObjectRepository, ComboBoxRepository>("comboBox");
            container.RegisterType<IPackObjectService, PackComboBoxService>("comboBox");
            container.RegisterType<IUnpackObjectService, UnpackComboBoxService>("comboBox");

            container.RegisterType<IPackObjectService, PackConfigurationService>("configuration");
            container.RegisterType<IUnpackObjectService, UnpackConfigurationService>("configuration");
            container.RegisterType<IInstallObjectService, InstallConfigurationService>("configuration");

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
            container.RegisterType<IPackObjectService, PackGridService>("grid");
            container.RegisterType<IUnpackObjectService, UnpackGridService>("grid");

            container.RegisterType<IInstallObjectService, InstallIconService>("icon");
            container.RegisterType<IPackObjectService, PackIconService>("icon");
            container.RegisterType<IUnpackObjectService, UnpackIconService>("icon");

            container.RegisterType<IInstallObjectService, InstallItemBoxService>("itemBox");
            container.RegisterType<IObjectRepository, ItemBoxRepository>("itemBox");
            container.RegisterType<IPackObjectService, PackItemBoxService>("itemBox");
            container.RegisterType<IUnpackObjectService, UnpackItemBoxService>("itemBox");

            container.RegisterType<IInstallObjectService, InstallReportService>("report");
            container.RegisterType<IPackObjectService, PackReportService>("report");
            container.RegisterType<IUnpackObjectService, UnpackReportService>("report");

            container.RegisterType<IInstallObjectService, InstallRepositoryService>("repository");
            container.RegisterType<IPackObjectService, PackRepositoryService>("repository");
            container.RegisterType<IUnpackObjectService, UnpackRepositoryService>("repository");

            container.RegisterType<IInstallObjectService, InstallRoleService>("role");
            container.RegisterType<IPackObjectService, PackRoleService>("role");
            container.RegisterType<IUnpackObjectService, UnpackRoleService>("role");

            container.RegisterType<IInstallObjectService, InstallSequenceService>("sequence");
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

            container.RegisterType<IInstallObjectService, InstallScriptService>("script");
            container.RegisterType<IPackObjectService, PackScriptService>("script");
            container.RegisterType<IUnpackObjectService, UnpackScriptService>("script");

            container.RegisterType<ISqlService, SqlService>();
            container.RegisterType<ISqlColumnService, SqlColumnService>();

            container.RegisterType<ILocalizationService, LocalizationService>();

            // For InstallRoleService
            container.RegisterType<IAuthorizationService, Authorization.Service.AuthorizationService>();
            container.RegisterType<ITrackingRepository, Tracking.Data.DB.TrackingRepository>();
            container.RegisterType<ITrackingService, Tracking.Service.TrackingService>();

            // For InstallSequenceService
            container.RegisterType<IOrganizationRepository, TenantSystem.Data.DB.OrganizationRepository>();
            container.RegisterType<Simplic.Configuration.IConfigurationRepository, Simplic.Configuration.Data.ConfigurationRepository>();
            container.RegisterType<Simplic.Configuration.IConfigurationService, Simplic.Configuration.Service.ConfigurationService>();
            container.RegisterType<Session.ISessionService, Session.Service.SessionService>();
            container.RegisterType<Cache.ICacheService, Cache.Service.CacheService>();
            container.RegisterType<IOrganizationService, TenantSystem.Service.OrganizationService>(); // TODO: Implement for SequenceService

            container.RegisterType<IIconService, IconService>();

            var serviceLocator = new UnityServiceLocator(container);
            CommonServiceLocator.ServiceLocator.SetLocatorProvider(() => serviceLocator);

            // For InstallReportService
            Base.GlobalSettings.UserName = "Package System";

            // CLI Value request services
            if (ApplicationSettings.ApplicationMode == ApplicationMode.CLI)
            {
                container.RegisterType<IRequestValueService, CliRequestConfigurationValueService>("configuration");
            }
            else if (ApplicationSettings.ApplicationMode == ApplicationMode.Test)
            {
                container.RegisterType<IRequestValueService, TestRequestConfigurationValueService>("configuration");
            }

            DALManager.Init(connectionString);
            ConnectionManager.Init(Thread.CurrentThread);

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
                    Guid = Guid.NewGuid(),
                    Name = name,
                    Version = new Version(1, 0, 0, 0),
                    Dependencies = new List<Dependency>(),
                    Objects = new Dictionary<string, IList<ObjectListItem>>()
                };

                var jsonSerializerSettings = new JsonSerializerSettings
                {
                    Converters = new List<JsonConverter> {
                        new VersionConverter()
                    },
                    Formatting = Formatting.Indented
                };

                var json = JsonConvert.SerializeObject(packageConfiguration, jsonSerializerSettings);
                File.WriteAllText(target + "package.json", json);
                Console.WriteLine($"Succesfully created a new package.json at {target}");
            }

            if (pack)
            {
                if (!File.Exists("package.json"))
                {
                    Console.WriteLine($"Couldent find a package.json file in {Path.GetFullPath(".")}");
                    return 1;
                }

                var fullPath = Path.GetFullPath("./package.json");
                var packService = container.Resolve<IPackService>();
                await packService.Pack(File.ReadAllText(fullPath), target);
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
            Console.WriteLine("Usage: simpack [Options]");
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