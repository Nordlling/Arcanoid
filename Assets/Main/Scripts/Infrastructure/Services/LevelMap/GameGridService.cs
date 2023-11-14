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
        private readonly IGameplayStateMachine _gameplayStateMachine;
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
            BlockPlaceInfo blockPlaceInfo = _currentLevel[block.GridPosition.x, block.GridPosition.y];
            blockPlaceInfo.Block = null;
            blockPlaceInfo.ID = 0;
            blockPlaceInfo.CheckToWin = false;

            CheckGridToWin();
        }

        public void ResetCurrentLevel()
        {
            _currentLevel = _blockPlacer.SpawnGrid(_levelMapInfo);
        }

        private void CheckGridToWin()
        {
            if (FindBlocksToWin())
            {
                _gameplayStateMachine.Enter<WinState>();
            }
        }

        private bool FindBlocksToWin()
        {
            for (int x = 0; x < _levelMapInfo.Width; x++)
            {
                for (int y = 0; y < _levelMapInfo.Height; y++)
                {
                    if (_currentLevel[x, y].CheckToWin)
                    {
                        return false;
                    }
                }
            }
            
            return true;
        }

        public void Restart()
        {
            DespawnBlocks();
            _currentLevel = _blockPlacer.SpawnGrid(_levelMapInfo);
        }

        private void DespawnBlocks()
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
        }
        
    }
}