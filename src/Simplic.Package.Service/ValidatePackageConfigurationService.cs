using System.Threading.Tasks;

namespace Simplic.Package.Service.Validate
{
    public class ValidatePackageConfigurationService : IValidatePackageConfigurationService
    {
        // TODO: Validate more
        public async Task<ValidatePackageConfigurationResult> Validate(PackageConfiguration packageConfiguration)
        {
            var validatePackageConfigurationResult = new ValidatePackageConfigurationResult
            {
                IsValid = true,
                LogLevel = LogLevel.Info,
                Message = "Package configuration is valid."
            };

            foreach (var item in packageConfiguration.Objects)
            {
                foreach (var objectListItem in item.Value)
                {
                    if (objectListItem.Mode == InstallMode.Migrate && objectListItem.Guid == null)
                    {
                        validatePackageConfigurationResult.IsValid = false;
                        validatePackageConfigurationResult.LogLevel = LogLevel.Error;
                        validatePackageConfigurationResult.Message = $"Objectlistitem with target {objectListItem.Target} is invalid, due to its InstallMode being Migrate and not having a Guid!";

                        return validatePackageConfigurationResult;
                    }
                }
            }
            return validatePackageConfigurationResult;
        }
    }
}