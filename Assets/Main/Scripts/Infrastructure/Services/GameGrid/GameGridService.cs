using Main.Scripts.Data;
using Main.Scripts.Factory;
using Main.Scripts.GameGrid;
using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.Infrastructure.Services.GameGrid.Loader;
using Main.Scripts.Infrastructure.Services.Packs;
using Main.Scripts.Logic.Blocks;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.GameGrid
{
    public class GameGridService : IGameGridService, IRestartable
    {
        private readonly IBlockFactory _blockFactory;
        private readonly ISimpleLoader _simpleLoader;
        private readonly IGameGridParser _gameGridParser;
        private readonly BlockPlacer _blockPlacer;
        private readonly IGameplayStateMachine _gameplayStateMachine;
        private readonly IPackService _packService;
        
        private GameGridInfo _gameGridInfo;
        private BlockPlaceInfo[,] _currentLevel;

        public GameGridService(
            IBlockFactory blockFactory,
            ISimpleLoader simpleLoader, 
            IGameGridParser gameGridParser, 
            BlockPlacer blockPlacer, 
            IGameplayStateMachine gameplayStateMachine,
            IPackService packService)
        {
            _blockFactory = blockFactory;
            _simpleLoader = simpleLoader;
            _gameGridParser = gameGridParser;
            _blockPlacer = blockPlacer;
            _gameplayStateMachine = gameplayStateMachine;
            _packService = packService;
        }
        
        public CurrentLevelInfo CurrentLevelInfo { get; set; }

        public void CreateLevelMap()
        {
            string currentLevelPath = _packService.GetCurrentLevelPath();
            if (string.IsNullOrEmpty(currentLevelPath))
            {
                Debug.LogWarning("No Current level info");
                return;
            }
            
            string json = _simpleLoader.LoadTextFile(currentLevelPath);
            if (string.IsNullOrEmpty(json))
            {
                return;
            }
            _gameGridInfo = _gameGridParser.ParseLevelMap(json);
            _currentLevel = _blockPlacer.SpawnGrid(_gameGridInfo);
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
            _currentLevel = _blockPlacer.SpawnGrid(_gameGridInfo);
        }

        private void CheckGridToWin()
        {
            if (FindBlocksToWin())
            {
                _packService.LevelUp();
                _gameplayStateMachine.Enter<WinState>();
            }
        }

        private bool FindBlocksToWin()
        {
            for (int x = 0; x < _gameGridInfo.Width; x++)
            {
                for (int y = 0; y < _gameGridInfo.Height; y++)
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
            CreateLevelMap();
        }

        private void DespawnBlocks()
        {
            for (int x = 0; x < _gameGridInfo.Width; x++)
            {
                for (int y = 0; y < _gameGridInfo.Height; y++)
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