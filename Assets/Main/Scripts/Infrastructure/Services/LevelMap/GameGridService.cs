using Main.Scripts.Configs;
using Main.Scripts.LevelMap;

namespace Main.Scripts.Infrastructure.Services.LevelMap
{
    public class GameGridService : IGameGridService
    {
        private readonly ILevelMapLoader _levelMapLoader;
        private readonly ILevelMapParser _levelMapParser;
        private readonly GameGridConfig _gameGridConfig;
        private readonly BlockPlacer _blockPlacer;
        private readonly AssetPathConfig _assetPathConfig;
        private LevelMapInfo _levelMapInfo;

        public GameGridService(
            ILevelMapLoader levelMapLoader, 
            ILevelMapParser levelMapParser, 
            BlockPlacer blockPlacer, 
            AssetPathConfig assetPathConfig,
            GameGridConfig gameGridConfig)
        {
            _levelMapLoader = levelMapLoader;
            _levelMapParser = levelMapParser;
            _blockPlacer = blockPlacer;
            _assetPathConfig = assetPathConfig;
            _gameGridConfig = gameGridConfig;
            CreateLevelMap();
        }

        private void CreateLevelMap()
        {
            string json = _levelMapLoader.LoadLevelMap(_assetPathConfig.LevelPacksPath, _gameGridConfig.DefaultLevel);
            _levelMapInfo = _levelMapParser.ParseLevelMap(json);
            _blockPlacer.SpawnGrid(_levelMapInfo);
        }
        
        
    }
}