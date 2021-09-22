using System.Threading.Tasks;

namespace Simplic.Package.Service
{
    /// <inheritdoc cref="IInitializePackageSystemService"/>
    public class InitializePackageSystemService : IInitializePackageSystemService
    {
        private readonly IInitializePackageSystemRepository initializePackageSystemRepository;
        private readonly ILogService logService;

        /// <summary>
        /// Initializes a new instance of <see cref="InitializePackageSystemService"/>.
        /// </summary>
        /// <param name="initializePackageSystemRepository"></param>
        /// <param name="logService"></param>
        public InitializePackageSystemService(IInitializePackageSystemRepository initializePackageSystemRepository, ILogService logService)
        {
            this.initializePackageSystemRepository = initializePackageSystemRepository;
            this.logService = logService;
        }

        /// <inheritdoc/>
        public async Task Initialize()
        {
            var result = await initializePackageSystemRepository.Initialize();
            await logService.WriteAsync(result.Message, result.LogLevel, result.Exception);
        }
    }
}