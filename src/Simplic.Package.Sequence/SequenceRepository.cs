using Dapper;
using Simplic.Framework.Core;
using Simplic.Sql;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Simplic.Package.Sequence
{
    public class SequenceRepository : IObjectRepository
    {
        private readonly ISqlService sqlService;

        public SequenceRepository(ISqlService sqlService)
        {
            this.sqlService = sqlService;
        }

        public Task<CheckMigrationResult> CheckMigration(InstallableObject installableObject)
        {
            throw new NotImplementedException();
        }

        public async Task<InstallObjectResult> InstallObject(InstallableObject installableObject)
        {
            if (installableObject.Content is DeserializedSequence deserializedSequence)
            {
                var result = new InstallObjectResult
                {
                    LogLevel = LogLevel.Info
                };

                try
                {
                    var sequence = new SequenceNumber
                    {
                        Id = deserializedSequence.Id,
                        InternName = deserializedSequence.InternalName,
                        DisplayName = deserializedSequence.DisplayName,
                        Format = deserializedSequence.Format
                    };

                    foreach (var counter in deserializedSequence.Counter)
                    {
                        sequence.Counters.Add(
                            new SequenceNumberCounter(sequence)
                            {
                                Id = counter.Id,
                                ValidFrom = counter.ValidFrom,
                                ValidTo = counter.ValidTo,
                                Minimum = counter.Min,
                                Maximum = counter.Max,
                                Step = counter.Step,
                                FixedLength = counter.FixedLength,
                                Format = counter.OptionalFormat,
                                TenantId = counter.TenenatId
                            });
                    }

                    SequenceNumberManager.Singleton.Save(sequence);

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

        public Task<UninstallObjectResult> UninstallObject(InstallableObject installableObject)
        {
            throw new NotImplementedException();
        }
    }
}