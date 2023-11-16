using Main.Scripts.Configs;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Infrastructure.Services.LevelMap;
using Main.Scripts.Infrastructure.Services.LevelMap.Loader;
using Main.Scripts.Infrastructure.Services.LevelMap.Parser;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Installers.ProjectInstallers
{
    public class PackInstaller : MonoInstaller
    {
        [SerializeField] private AssetPathConfig _assetPathConfig;
        
        public override void InstallBindings(ServiceContainer serviceContainer)
        {
            RegisterPackService(serviceContainer);
        }

        private void RegisterPackService(ServiceContainer serviceContainer)
        {
            PackService packService = new PackService(_assetPathConfig, new SimpleLoader(), new SimpleParser());
            serviceContainer.SetService<IPackService, PackService>(packService);
        }
    }
}