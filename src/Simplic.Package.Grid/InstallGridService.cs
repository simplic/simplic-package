using AutoMapper;
using Simplic.Framework.DBUI;
using Simplic.UI.GridView;
using System;
using System.Threading.Tasks;

namespace Simplic.Package.Grid
{
    /// <summary>
    /// Service to install a grid.
    /// </summary>
    public class InstallGridService : IInstallObjectService
    {
        private readonly ILogService logService;

        public InstallGridService(ILogService logService)
        {
            this.logService = logService;
        }

        /// <inheritdoc/>
        public async Task<InstallObjectResult> InstallObject(InstallableObject installableObject)
        {
            if (installableObject.Content is Grid grid)
            {
                var result = new InstallObjectResult { Success = true };

                var mapConfig = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Grid, GridContainerModel>(MemberList.None)
                        .ForMember(dest => dest.ReloadGridButtonVisibility, opt => opt.MapFrom(x => x.ReloadGridButtonVisibility));
                    cfg.CreateMap<GridMenuConfiguration, GridMenuConfigurationModel>(MemberList.None);
                    cfg.CreateMap<GridCellSqlHighlight, GridCellSqlHighlightModel>(MemberList.None);
                    cfg.CreateMap<DivergentColumnType, IDivergentColumnType>(MemberList.None);


                    cfg.CreateMap<ColumnConfiguration, ColumnConfigurationModel>(MemberList.Source).As<GridColumnConfigurationModel>();

                    cfg.CreateMap<ColumnConfiguration, GridColumnConfigurationModel>(MemberList.Source);

                    cfg.CreateMap<GridProfileConfiguration, GridProfileConfigurationModel>(MemberList.None);

                    cfg.CreateMap<GridVirtualGroupConfiguration, Simplic.UI.GridView.GridVirtualGroupConfiguration>(MemberList.None)
                        .IgnoreAllPropertiesWithAnInaccessibleSetter();
                    cfg.CreateMap<VirtualGroupDefinition, Simplic.Framework.DBUI.VirtualGroupDefinition>(MemberList.None)
                        .ForMember(x => x.Name, opt => opt.Ignore());
                });

                try
                {
                    mapConfig.AssertConfigurationIsValid();
                    var mapper = new Mapper(mapConfig);
                    var containerModel = mapper.Map<GridContainerModel>(installableObject.Content);

                    GridViewManager.Singleton.SaveConfiguration(containerModel);

                    result.Success = true;

                    await logService.WriteAsync($"Installed Grid at {installableObject.Target}.", LogLevel.Info);

                }
                catch (Exception ex)
                {
                    await logService.WriteAsync($"Failed to install Grid at {installableObject.Target}.", LogLevel.Error, ex);

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