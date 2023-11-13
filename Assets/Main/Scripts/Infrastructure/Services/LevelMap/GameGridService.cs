using Main.Scripts.Configs;
using Main.Scripts.Data;
using Main.Scripts.Factory;
using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.LevelMap;
using Main.Scripts.Logic.Blocks;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.LevelMap
{
    public class GameGridService : IGameGridService, IRestartable
    {
        private readonly IBlockFactory _blockFactory;
        private readonly ILevelMapLoader _levelMapLoader;
        private readonly ILevelMapParser _levelMapParser;
        private readonly BlockPlacer _blockPlacer;
        private IGameplayStateMachine _gameplayStateMachine;
        private readonly AssetPathConfig _assetPathConfig;
        
        private LevelMapInfo _levelMapInfo;
        private BlockPlaceInfo[,] _currentLevel;

        public GameGridService(
            IBlockFactory blockFactory,
            ILevelMapLoader levelMapLoader, 
            ILevelMapParser levelMapParser, 
            BlockPlacer blockPlacer, 
            IGameplayStateMachine gameplayStateMachine,
            AssetPathConfig assetPathConfig)
        {
            _blockFactory = blockFactory;
            _levelMapLoader = levelMapLoader;
            _levelMapParser = levelMapParser;
            _blockPlacer = blockPlacer;
            _gameplayStateMachine = gameplayStateMachine;
            _assetPathConfig = assetPathConfig;
        }
        
        public CurrentLevelInfo CurrentLevelInfo { get; set; }

        public void CreateLevelMap()
        {
            if (CurrentLevelInfo is null)
            {
                Debug.LogError("No Current level info");
                return;
            }
            
            string json = _levelMapLoader.LoadLevelMap(_assetPathConfig.LevelPacksPath, CurrentLevelInfo);
            _levelMapInfo = _levelMapParser.ParseLevelMap(json);
            _currentLevel = _blockPlacer.SpawnGrid(_levelMapInfo);
        }

        public void RemoveBlockFromGrid(Block block)
        {
            bool anyBlock = false;
            for (int x = 0; x < _levelMapInfo.Width; x++)
            {
                for (int y = 0; y < _levelMapInfo.Height; y++)
                {
                    if (_currentLevel[x, y].Block == block)
                    {
                        _currentLevel[x, y].Block = null;
                        _currentLevel[x, y].ID = 0;
                        continue;
                    }

                    if (!anyBlock && _currentLevel[x, y].ID != 0)
                    {
                        anyBlock = true;
                    }
                } 
            }

            if (!anyBlock)
            {
                _gameplayStateMachine.Enter<WinState>();
            }
        }

        public void ResetCurrentLevel()
        {
            _currentLevel = _blockPlacer.SpawnGrid(_levelMapInfo);
        }

        public void Restart()
        {
            for (int x = 0; x < _levelMapInfo.Width; x++)
            {
                for (int y = 0; y < _levelMapInfo.Height; y++)
                {
                    if (_currentLevel[x, y].Block != null)
                    {
                        _blockFactory.Despawn(_currentLevel[x, y].Block);
                    }
                }
            }
            
            _currentLevel = _blockPlacer.SpawnGrid(_levelMapInfo);
        }
    }

    public class BlockPlaceInfo
    {
        public int ID;
        public Block Block;
        
        public BlockPlaceInfo(int id, Block block)
        {
            ID = id;
            Block = block;
        }
        
    }
}