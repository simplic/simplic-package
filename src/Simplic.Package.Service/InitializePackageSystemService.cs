using System.Threading.Tasks;

namespace Simplic.Package.Service
{
    public class InitializePackageSystemService : IInitializePackageSystemService
    {
        private readonly IInitializePackageSystemRepository initializePackageSystemRepository;

        public InitializePackageSystemService(IInitializePackageSystemRepository initializePackageSystemRepository)
        {
            this.initializePackageSystemRepository = initializePackageSystemRepository;
        }

        public async Task<InitializePackageSystemResult> Initialize()
        {
            return await initializePackageSystemRepository.Intialize();
        }
    }
}