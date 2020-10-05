using Simplic.Framework.Reporting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package.EplReport
{
    public class EplReportRepository : IObjectRepository
    {
        public Task<CheckMigrationResult> CheckMigration(InstallableObject installableObject)
        {
            throw new NotImplementedException();
        }

        public Task<InstallObjectResult> InstallObject(InstallableObject installableObject)
        {
            if (installableObject.Content is DeserializedEplReport eplReport)
            {
                var result = new InstallObjectResult
                {
                    LogLevel = LogLevel.Info
                };

                try
                {
                    EPLReportManager.Singleton.Save(new EPLReport
                    {
                        Id = eplReport.Id,
                        PrinterName = eplReport.Printer,
                        IsContextlessPrintable = eplReport.IsContextlessPrintable,
                        DataSourceType = eplReport.Type,
                    })
                }
            }
            throw new InvalidContentException();
        }
    }
}
