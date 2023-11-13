using Main.Scripts.Configs;
using Main.Scripts.Factory;
using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Infrastructure.Services.LevelMap;
using Main.Scripts.LevelMap;
using Main.Scripts.Logic.GameGrid;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Installers
{
    public class GameGridInstaller : MonoInstaller
    {
        [Header("Configs")] 
        [SerializeField] private TiledBlockConfig _tiledBlockConfig;
        [SerializeField] private GameGridConfig _gameGridConfig;
        [SerializeField] private AssetPathConfig _assetPathConfig;

        [Header("Prefabs")]
        
        [Header("Scene Objects")]
        [SerializeField] private ZonesManager _zonesManager;


        public override void InstallBindings(ServiceContainer serviceContainer)
        {
            RegisterZonesManager(serviceContainer);
            RegisterGameGridService(serviceContainer);
            InitGameGridService(serviceContainer);
        }

        private void RegisterZonesManager(ServiceContainer serviceContainer)
        {
            _zonesManager.Init();
            serviceContainer.SetServiceSelf(_zonesManager);
        }

        private void RegisterGameGridService(ServiceContainer serviceContainer)
        {
            BlockPlacer blockPlacer = new BlockPlacer(
                serviceContainer.Get<IBlockFactory>(),
                serviceContainer.Get<ZonesManager>(),
                _gameGridConfig);

            GameGridLoader gameGridLoader = new GameGridLoader();
            GameGridParser gameGridParser = new GameGridParser();
            GameGridService gameGridService = new GameGridService
                (
                    serviceContainer.Get<IBlockFactory>(), 
                    gameGridLoader,
                    gameGridParser, 
                    blockPlacer, 
                    serviceContainer.Get<IGameplayStateMachine>(), 
                    _assetPathConfig
                );
            
            serviceContainer.SetService<IGameGridService, GameGridService>(gameGridService);
            
            serviceContainer.Get<IGameplayStateMachine>().AddGameplayStatable(gameGridService);
        }

        private void InitGameGridService(ServiceContainer serviceContainer)
        {
            serviceContainer.Get<IGameGridService>().CurrentLevelInfo = _gameGridConfig.DefaultLevel;
            serviceContainer.Get<IGameGridService>().CreateLevelMap();
        }
    }
}