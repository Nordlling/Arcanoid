using Main.Scripts.Configs;
using Main.Scripts.Factory;
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
        [SerializeField] private GameGridZone _gameGridZone;


        public override void InstallBindings(ServiceContainer serviceContainer)
        {
            RegisterGameGridService(serviceContainer);
        }

        private void RegisterGameGridService(ServiceContainer serviceContainer)
        {
            BlockPlacer blockPlacer = new BlockPlacer(
                serviceContainer.Get<IFactoryContainer>(),
                _gameGridZone,
                _gameGridConfig);

            GameGridLoader gameGridLoader = new GameGridLoader();
            GameGridParser gameGridParser = new GameGridParser();
            GameGridService gameGridService = new GameGridService(gameGridLoader, gameGridParser, blockPlacer, _assetPathConfig, _gameGridConfig);
            serviceContainer.SetService<ILevelMapService, GameGridService>(gameGridService);
        }
    }
}