using Simplic.Framework.Core;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Simplic.Package.Sequence
{
    public class SequenceRepository : IObjectRepository
    {
        public Task<CheckMigrationResult> CheckMigration(InstallableObject installableObject)
        {
            throw new NotImplementedException();
        }

        public async Task<InstallObjectResult> InstallObject(InstallableObject installableObject)
        {
            if (installableObject.Content is DeserializedSequence sequence)
            {
                var result = new InstallObjectResult
                {
                    LogLevel = LogLevel.Info
                };

                try
                {
                    var counters = sequence.Counter.Select(x => new SequenceNumberCounter(new SequenceNumber())
                    {
                        Id = x.Id,
                        ValidFrom = x.ValidFrom,
                        ValidTo = x.ValidTo,
                        Minimum = x.Min,
                        Maximum = x.Max,
                        Step = x.Step,
                        FixedLength = x.FixedLength,
                        Format = x.OptionalFormat,
                        TenantId = x.TenenatId
                    });

                    SequenceNumberManager.Singleton.Save(new SequenceNumber()
                    {
                        Counters = counters,
                    });
                    result.Message = $"Installed Sequence at {installableObject.Target}.";
                    result.Success = true;
                }
                catch (Exception ex)
                {
                    result.Message = $"Failed to install Sequence at {installableObject.Target}.";
                    result.LogLevel = LogLevel.Error;
                    result.Exception = ex;
                }
                return result;
            }
            throw new InvalidContentException();
        }
    }
}