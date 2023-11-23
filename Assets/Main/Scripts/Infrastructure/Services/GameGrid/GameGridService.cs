using System;
using Main.Scripts.Data;
using Main.Scripts.Factory;
using Main.Scripts.GameGrid;
using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.Infrastructure.Services.Difficulty;
using Main.Scripts.Infrastructure.Services.Energies;
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
        private readonly IDifficultyService _difficultyService;
        private readonly IEnergyService _energyService;

        private GameGridInfo _gameGridInfo;
        private BlockPlaceInfo[,] _currentLevel;

        public int AllBlocks { get; private set; }
        public int AllBlocksToWin { get; private set; }
        public int DestroyedBlocksToWin { get; private set; }
        public Vector2Int GridSize => _gameGridInfo.Size;

        public GameGridService(
            IBlockFactory blockFactory,
            ISimpleLoader simpleLoader, 
            IGameGridParser gameGridParser, 
            BlockPlacer blockPlacer, 
            IGameplayStateMachine gameplayStateMachine,
            IPackService packService,
            IDifficultyService difficultyService,
            IEnergyService energyService)
        {
            _blockFactory = blockFactory;
            _simpleLoader = simpleLoader;
            _gameGridParser = gameGridParser;
            _blockPlacer = blockPlacer;
            _gameplayStateMachine = gameplayStateMachine;
            _packService = packService;
            _difficultyService = difficultyService;
            _energyService = energyService;
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
        
        public bool TryGet(out Block block, Vector2Int position)
        {
            block = null;
            if (position.x < 0 || position.x >= _currentLevel.GetLength(0) ||
                position.y < 0 || position.y >= _currentLevel.GetLength(1))
            {
                return false;
            }

            if (_currentLevel[position.x, position.y].Block == null)
            {
                return false;
            }

            block = _currentLevel[position.x, position.y].Block;
            return true;

        }

        public void RemoveAt(Block block)
        {
            RemoveAt(new Vector2Int(block.GridPosition.x, block.GridPosition.y));
        }

        public void RemoveAt(Vector2Int position)
        {
            if (IsWithinArrayBounds(_currentLevel, position.x, position.y))
            {
                RemoveBlock(_currentLevel[position.x, position.y]);
            }
        }

        private void RemoveBlock(BlockPlaceInfo blockPlaceInfo)
        {
            if (blockPlaceInfo.Block == null || blockPlaceInfo.ID == 0)
            {
                return;
            }
            
            _blockFactory.Despawn(blockPlaceInfo.Block);
            
            if (blockPlaceInfo.CheckToWin)
            {
                DestroyedBlocksToWin++;
                _difficultyService.IncreaseDifficulty(DestroyedBlocksToWin, AllBlocksToWin);
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
                _energyService.RewardEnergy();
                _gameplayStateMachine.Enter<WinState>();
            }
        }

        private void InitGrid()
        {
            _currentLevel = _blockPlacer.SpawnGrid(_gameGridInfo);
            AllBlocks = 0;
            AllBlocksToWin = 0;
            DestroyedBlocksToWin = 0;
            
            for (int x = 0; x < _gameGridInfo.Size.x; x++)
            {
                for (int y = 0; y < _gameGridInfo.Size.y; y++)
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
            for (int x = 0; x < _gameGridInfo.Size.x; x++)
            {
                for (int y = 0; y < _gameGridInfo.Size.y; y++)
                {
                    if (_currentLevel[x, y].Block != null)
                    {
                        _blockFactory.Despawn(_currentLevel[x, y].Block);
                    }
                }
            }
        }
        
        private bool IsWithinArrayBounds(BlockPlaceInfo[,] array, params int[] positions)
        {
            for (int i = 0; i < positions.Length; i++)
            {
                if (positions[i] < 0 || positions[i] >= array.GetLength(i))
                {
                    return false;
                }
            }

            return true;
        }
        
    }
}