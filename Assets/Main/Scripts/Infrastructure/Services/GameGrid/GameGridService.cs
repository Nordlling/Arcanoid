using System;
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

        public int AllBlocks { get; private set; }
        public int AllBlocksToWin { get; private set; }
        public int DestroyedBlocksToWin { get; private set; }
        

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

        public event Action OnDestroyed;

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
                Debug.LogWarning("Can't load level info");
                return;
            }
            
            _gameGridInfo = _gameGridParser.ParseLevelMap(json);
            InitGrid();
        }

        public void RemoveBlockFromGrid(Block block)
        {
            BlockPlaceInfo blockPlaceInfo = _currentLevel[block.GridPosition.x, block.GridPosition.y];

            if (blockPlaceInfo.CheckToWin)
            {
                DestroyedBlocksToWin++;
            }
            
            blockPlaceInfo.Block = null;
            blockPlaceInfo.ID = 0;
            blockPlaceInfo.CheckToWin = false;
           
            OnDestroyed?.Invoke();
            CheckGridToWin();
        }

        public void ResetCurrentLevel()
        {
            InitGrid();
        }

        private void CheckGridToWin()
        {
            if (DestroyedBlocksToWin >= AllBlocksToWin)
            {
                _packService.LevelUp();
                _gameplayStateMachine.Enter<WinState>();
            }
        }

        private void InitGrid()
        {
            _currentLevel = _blockPlacer.SpawnGrid(_gameGridInfo);
            AllBlocks = 0;
            AllBlocksToWin = 0;
            DestroyedBlocksToWin = 0;
            
            for (int x = 0; x < _gameGridInfo.Width; x++)
            {
                for (int y = 0; y < _gameGridInfo.Height; y++)
                {
                    if (_currentLevel[x, y].ID == 0)
                    {
                        continue;
                    }
                    
                    AllBlocks++;
                    if (_currentLevel[x, y].CheckToWin)
                    {
                        AllBlocksToWin++;
                    }
                }
            }
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