using System;
using System.Threading.Tasks;
using Main.Scripts.Factory;
using Main.Scripts.GameGrid;
using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.Infrastructure.Installers;
using Main.Scripts.Infrastructure.Services.Difficulty;
using Main.Scripts.Infrastructure.Services.Energies;
using Main.Scripts.Infrastructure.Services.GameGrid.Loader;
using Main.Scripts.Infrastructure.Services.Packs;
using Main.Scripts.Logic.Blocks;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.GameGrid
{
    public class GameGridService : IGameGridService, IInitializable
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
        private bool _isWin;
        
        public BlockPlaceInfo[,] CurrentLevel { get; private set; }
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
        
        public event Action OnDestroyed;

        public void Init()
        {
            CreateLevelMap();
        }

        public bool TryGetBlock(out Block block, Vector2Int gridPosition)
        {
            block = null;
            if (!IsWithinArrayBounds(gridPosition))
            {
                return false;
            }

            if (CurrentLevel[gridPosition.x, gridPosition.y].Block == null)
            {
                return false;
            }

            block = CurrentLevel[gridPosition.x, gridPosition.y].Block;
            return true;

        }

        public bool TryGetWorldPosition(out Vector2 worldPosition, Vector2Int gridPosition)
        {
            worldPosition = Vector2.zero;
            
            if (!IsWithinArrayBounds(gridPosition))
            {
                return false;
            }

            worldPosition = CurrentLevel[gridPosition.x, gridPosition.y].WorldPosition;
            return true;
        }

        public bool TryGetBlockPlaceInfo(out BlockPlaceInfo blockPlaceInfo, Vector2Int gridPosition)
        {
            blockPlaceInfo = null;
            
            if (!IsWithinArrayBounds(gridPosition))
            {
                return false;
            }

            blockPlaceInfo = CurrentLevel[gridPosition.x, gridPosition.y];
            return true;
        }

        public bool TryRemoveAt(Block block, out BlockPlaceInfo blockPlaceInfo)
        {
            return TryRemoveAt(new Vector2Int(block.GridPosition.x, block.GridPosition.y), out blockPlaceInfo);
        }

        public bool TryRemoveAt(Vector2Int position, out BlockPlaceInfo blockPlaceInfo)
        {
            blockPlaceInfo = null;
            
            if (!IsWithinArrayBounds(position))
            {
                return false;
            }
            
            blockPlaceInfo = CurrentLevel[position.x, position.y];
            RemoveBlock(blockPlaceInfo);
            return true;
        }

        public bool IsWithinArrayBounds(Vector2Int position)
        {
            return position.x >= 0 && position.x < CurrentLevel.GetLength(0) 
                                   && position.y >= 0 && position.y < CurrentLevel.GetLength(1);
        }

        public void RestartLevel()
        {
            DespawnBlocks();
            CreateLevelMap();
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

        private void CreateLevelMap()
        {
            _isWin = false;
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

        private void CheckGridToWin()
        {
            if (_isWin || DestroyedBlocksToWin < AllBlocksToWin)
            {
                return;
            }

            _isWin = true;
            _packService.LevelUp();
            _energyService.RewardEnergy(_energyService.RewardForPass);
            _gameplayStateMachine.Enter<WinState>();
        }

        private void InitGrid()
        {
            CurrentLevel = _blockPlacer.SpawnGrid(_gameGridInfo);
            AllBlocks = 0;
            AllBlocksToWin = 0;
            DestroyedBlocksToWin = 0;
            
            for (int x = 0; x < _gameGridInfo.Size.x; x++)
            {
                for (int y = 0; y < _gameGridInfo.Size.y; y++)
                {
                    if (CurrentLevel[x, y].ID == 0)
                    {
                        continue;
                    }
                    
                    AllBlocks++;
                    if (CurrentLevel[x, y].CheckToWin)
                    {
                        AllBlocksToWin++;
                    }
                }
            }
        }

        private void DespawnBlocks()
        {
            for (int x = 0; x < _gameGridInfo.Size.x; x++)
            {
                for (int y = 0; y < _gameGridInfo.Size.y; y++)
                {
                    if (CurrentLevel[x, y].Block is not null)
                    {
                        _blockFactory.Despawn(CurrentLevel[x, y].Block);
                    }
                }
            }
        }
    }
}