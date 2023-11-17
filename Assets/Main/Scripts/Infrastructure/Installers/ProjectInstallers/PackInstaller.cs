using Main.Scripts.Configs;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Infrastructure.Services.GameGrid.Loader;
using Main.Scripts.Infrastructure.Services.GameGrid.Parser;
using Main.Scripts.Infrastructure.Services.Packs;
using Main.Scripts.Infrastructure.Services.SaveLoad;
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
            PackService packService = new PackService(_assetPathConfig, new SimpleLoader(), new SimpleParser(), serviceContainer.Get<ISaveLoadService>());
            serviceContainer.SetService<IPackService, PackService>(packService);
        }
    }
}