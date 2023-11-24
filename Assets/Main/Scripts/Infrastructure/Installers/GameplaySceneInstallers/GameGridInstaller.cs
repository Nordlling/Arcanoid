using Main.Scripts.Configs;
using Main.Scripts.Factory;
using Main.Scripts.GameGrid;
using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Infrastructure.Services.Difficulty;
using Main.Scripts.Infrastructure.Services.Energies;
using Main.Scripts.Infrastructure.Services.GameGrid;
using Main.Scripts.Infrastructure.Services.GameGrid.Loader;
using Main.Scripts.Infrastructure.Services.Packs;
using Main.Scripts.Logic.Zones;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Installers.GameplaySceneInstallers
{
    public class GameGridInstaller : MonoInstaller
    {
        [Header("Configs")] 
        [SerializeField] private GameGridConfig _gameGridConfig;

        [Header("Prefabs")]
        
        [Header("Scene Objects")]
        [SerializeField] private ZonesManager _zonesManager;


        public override void InstallBindings(ServiceContainer serviceContainer)
        {
            RegisterZonesManager(serviceContainer);
            RegisterGameGridService(serviceContainer);
        }

        private void RegisterZonesManager(ServiceContainer serviceContainer)
        {
            _zonesManager.Init();
            serviceContainer.SetService<ITickable, ZonesManager>(_zonesManager);
            serviceContainer.SetServiceSelf(_zonesManager);
        }

        private void RegisterGameGridService(ServiceContainer serviceContainer)
        {
            BlockPlacer blockPlacer = new BlockPlacer(
                serviceContainer.Get<IBlockFactory>(),
                serviceContainer.Get<ZonesManager>(),
                _gameGridConfig);

            SimpleLoader simpleLoader = new SimpleLoader();
            GameGridParser gameGridParser = new GameGridParser();
            GameGridService gameGridService = new GameGridService
                (
                    serviceContainer.Get<IBlockFactory>(), 
                    simpleLoader,
                    gameGridParser, 
                    blockPlacer, 
                    serviceContainer.Get<IGameplayStateMachine>(), 
                    serviceContainer.Get<IPackService>(),
                    serviceContainer.Get<IDifficultyService>(),
                    serviceContainer.Get<IEnergyService>()
                );

            serviceContainer.SetService<IGameGridService, GameGridService>(gameGridService);
            serviceContainer.SetService<IInitializable, GameGridService>(gameGridService);

            serviceContainer.Get<IGameplayStateMachine>().AddGameplayStatable(gameGridService);
        }
    }
}