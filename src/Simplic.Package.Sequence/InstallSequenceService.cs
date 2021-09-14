using Simplic.Framework.Core;
using System;
using System.Threading.Tasks;

namespace Simplic.Package.Sequence
{
    /// <summary>
    /// Service to install a sequence.
    /// </summary>
    public class InstallSequenceService : IInstallObjectService
    {
        private readonly ILogService logService;

        /// <summary>
        /// Initializes a new instance of <see cref="InstallSequenceService"/>
        /// </summary>
        /// <param name="logService"></param>
        public InstallSequenceService(ILogService logService)
        {
            this.logService = logService;
        }

        /// <inheritdoc/>
        public async Task<InstallObjectResult> InstallObject(InstallableObject installableObject)
        {
            if (installableObject.Content is Sequence deserializedSequence)
            {
                var result = new InstallObjectResult { Success = true };

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
                                TenantId = counter.TenantId
                            });
                    }

                    SequenceNumberManager.Singleton.Save(sequence);

                    await logService.WriteAsync($"Installed Sequence at {installableObject.Target}.", LogLevel.Info);
                    result.Success = true;
                }
                catch (Exception ex)
                {
                    await logService.WriteAsync($"Failed to install Sequence at {installableObject.Target}.", LogLevel.Error, ex);

                    result.Success = false;
                }
                return result;
            }
            throw new InvalidContentException();
        }

        /// <inheritdoc/>
        public Task<UninstallObjectResult> UninstallObject(InstallableObject installableObject)
        {
            throw new NotImplementedException();
        }
    }
}