using AutoMapper;
using Simplic.Framework.DBUI;
using Simplic.UI.GridView;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Simplic.Package.Grid
{
    public class InstallGridService : IInstallObjectService
    {
        /*
        private readonly IObjectRepository repository;

        public InstallGridService([Dependency("grid")] IObjectRepository repository)
        {
            this.repository = repository;
        }*/

        public async Task<CheckMigrationResult> CheckMigration(InstallableObject installableObject)
        {
            throw new NotImplementedException();
        }

        public async Task<InstallObjectResult> InstallObject(InstallableObject installableObject)
        {
            if (installableObject.Content is DeserializedGrid grid)
            {
                var result = new InstallObjectResult
                {
                    LogLevel = LogLevel.Info
                };

                var mapConfig = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<DeserializedGrid, GridContainerModel>(MemberList.None)
                        .ForMember(dest => dest.ReloadGridButtonVisibility, opt => opt.MapFrom(x => x.ReloadGridButtonVisibility));
                    cfg.CreateMap<GridMenuConfiguration, GridMenuConfigurationModel>(MemberList.None);
                    cfg.CreateMap<GridCellSqlHighlight, GridCellSqlHighlightModel>(MemberList.None);
                    cfg.CreateMap<DivergentColumnType, IDivergentColumnType>(MemberList.None);


                    cfg.CreateMap<ColumnConfiguration, ColumnConfigurationModel>(MemberList.Source)
                            .Include<ColumnConfiguration, GridColumnConfigurationModel>();

                    cfg.CreateMap<ColumnConfiguration, GridColumnConfigurationModel>(MemberList.Source);

                    cfg.CreateMap<GridProfileConfiguration, GridProfileConfigurationModel>(MemberList.None);

                    cfg.CreateMap<GridVirtualGroupConfiguration, Simplic.UI.GridView.GridVirtualGroupConfiguration>(MemberList.None)
                        .ForMember(x => x.ViewDataTemplate, opt => opt.Ignore())
                        .ForMember(x => x.EditDataTemplate, opt => opt.Ignore())
                        .ForMember(x => x.ViewDataTemplatePath, opt => opt.Ignore())
                        .ForMember(x => x.EditDataTemplatePath, opt => opt.Ignore())
                        .ForMember(x => x.ItemFilterText, opt => opt.Ignore());
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
                    result.Message = $"Installed Grid at {installableObject.Target}.";

                }
                catch (Exception ex)
                {
                    result.Message = $"Failed to install Grid at {installableObject.Target}.";
                    result.LogLevel = LogLevel.Error;
                    result.Exception = ex;
                }
                return result;
            }
            throw new InvalidContentException();
        }

        public Task OverwriteObject(InstallableObject installableObject)
        {
            throw new NotImplementedException();
        }

        public Task<UninstallObjectResult> UninstallObject(InstallableObject installableObject)
        {
            throw new NotImplementedException();
        }
    }
}