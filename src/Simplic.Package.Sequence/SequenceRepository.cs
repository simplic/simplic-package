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

                    var success = await sqlService.OpenConnection(async (c) =>
                    {
                        var affectedRows = await c.ExecuteAsync("Insert into SequenceNumber (id, internname, displayname, format) " +
                                                            "on existing values (:id, :internalname, :displayname, :format)",
                                                            new { sequence.Id, sequence.InternalName, sequence.DisplayName, sequence.Format });
                        return affectedRows > 0;
                    });

                    if (success)
                    {
                        var insertedCounters = 0;
                        foreach (var counter in sequence.Counter)
                        {
                            allot of the counter information is missing in the table
                            insertedCounters += await sqlService.OpenConnection(async (c) =>
                            {
                                return await c.ExecuteAsync("Insert into SequenceNumber_Counter (id, validfrom, validto, fixedlength, optionalformat, dbsequencename, sequenceid, currentposition, tenantid)" +
                                                                                " on existing update values (",
                                                                                new { counter.Id, counter.ValidFrom, counter.ValidTo, counter.FixedLength, counter.OptionalFormat, sequence.Id, });
                            });
                        }
                    }

                    SequenceNumberManager.Singleton.Save(new SequenceNumber()
                    {
                        Counters = counters,
                    });

                    SequenceNumberManager.Singleton.Save(counters)

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