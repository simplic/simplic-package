using System.Reflection;
using System.Threading.Tasks;

namespace Simplic.Package.Service
{
    public class InitializePackageSystemService : IInitializePackageSystemService
    {
        private readonly IInitializePackageSystemRepository initializePackageSystemRepository;
        private readonly ILogService logService;

        public InitializePackageSystemService(IInitializePackageSystemRepository initializePackageSystemRepository, ILogService logService)
        {
            this.initializePackageSystemRepository = initializePackageSystemRepository;
            this.logService = logService;
        }

        public async Task Initialize()
        {
            var result = await initializePackageSystemRepository.Initialize();
            await logService.WriteAsync(result.Message, result.LogLevel, result.Exception);
        }
    }
}