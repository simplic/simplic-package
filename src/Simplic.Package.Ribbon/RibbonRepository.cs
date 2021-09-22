using Dapper;
using Simplic.Sql;
using System;
using System.Threading.Tasks;

namespace Simplic.Package.Ribbon
{
    /// <summary>
    /// Repository to check and read ribbon items from the db.
    /// </summary>
    public class RibbonRepository : IObjectRepository
    {
        private readonly ISqlService sqlService;
        private readonly ILogService logService;

        /// <summary>
        /// Initializes a new instance of <see cref="RibbonRepository"/>.
        /// </summary>
        /// <param name="sqlService"></param>
        /// <param name="logService"></param>
        public RibbonRepository(ISqlService sqlService, ILogService logService)
        {
            this.sqlService = sqlService;
            this.logService = logService;
        }

        /// <inheritdoc/>
        public async Task<InstallObjectResult> InstallObject(InstallableObject installableObject)
        {
            if (installableObject.Content is RibbonTab ribbon)
            {
                var result = new InstallObjectResult { Success = true };

                try
                {
                    var success = await sqlService.OpenConnection(async (c) =>
                    {
                        var affectedRows = await c.ExecuteAsync("INSERT INTO RibbonMenu_Tab(Guid, TabHeader, OrderNr) ON EXISTING UPDATE VALUES (:Guid, :TabHeader, :OrderNr)",
                                                                 new { Guid = ribbon.Id, TabHeader = ribbon.Name, OrderNr = ribbon.OrderId });

                        if (ribbon.Groups != null)
                        {
                            foreach (var group in ribbon.Groups)
                            {
                                affectedRows += await c.ExecuteAsync("INSERT INTO RibbonMenu_Group(Guid, TabGuid, GroupHeader, OrderNr) ON EXISTING UPDATE VALUES (:Guid, :TabGuid, :GroupHeader, :OrderNr)",
                                                                     new { Guid = group.Id, TabGuid = ribbon.Id, GroupHeader = group.Name, OrderNr = ribbon.OrderId });
                            }
                        }

                        return affectedRows > 0;
                    });

                    if (success)
                    {
                        result.Success = true;
                        await logService.WriteAsync($"Installed RibbonTab/RibbonGroup at {installableObject.Target}.", LogLevel.Info);
                    }
                    else
                    {
                        await logService.WriteAsync($"Failed to install RibbonTab/RibbonGroup at {installableObject.Target}.", LogLevel.Warning);
                    }
                }
                catch (Exception ex)
                {
                    await logService.WriteAsync($"Failed to install RibbonTab/RibbonGroup at {installableObject.Target}.", LogLevel.Error, ex);

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