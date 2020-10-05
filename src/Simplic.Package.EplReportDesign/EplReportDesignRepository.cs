using Simplic.Framework.Reporting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package.EplReportDesign
{
    public class EplReportDesignRepository : IObjectRepository
    {
        public Task<CheckMigrationResult> CheckMigration(InstallableObject installableObject)
        {
            throw new NotImplementedException();
        }

        public Task<InstallObjectResult> InstallObject(InstallableObject installableObject)
        {
            if (installableObject.Content is DeserializedEplReportDesign eplReportDesign)
            {
                var result = new InstallObjectResult
                {
                    LogLevel = LogLevel.Info
                };

                try
                {
                    EPLReportManager.Singleton.SaveDesign(new EPLDesignModel
                    {
                    });
                }
            }
            throw new InvalidContentException();
        }
    }
}
