using System.Collections.Generic;
using System.Linq;
using Main.Scripts.Configs.Boosts;
using Main.Scripts.Factory;
using Main.Scripts.Infrastructure.GameplayStates;
using Main.Scripts.Infrastructure.Installers;
using Main.Scripts.Infrastructure.Provides;
using Main.Scripts.Infrastructure.Services.GameGrid;
using Main.Scripts.Logic.Blocks;
using Main.Scripts.Logic.Boosts;
using Main.Scripts.Logic.Effects;
using UnityEngine;

namespace Main.Scripts.Logic.Explosions
{
    public class ExplosionSystem : IExplosionSystem, ITickable, IRestartable
    {
        private readonly IGameGridService _gameGridService;
        private readonly IEffectFactory _effectFactory;
        private readonly ITimeProvider _timeProvider;

        private readonly SpawnContext _spawnContext = new();
        private readonly Dictionary<BlockPlaceInfo, ExplosionInfo> _explosions = new();

        public ExplosionSystem(
            IGameGridService gameGridService, 
            IEffectFactory effectFactory,
            ITimeProvider timeProvider)
        {
            _gameGridService = gameGridService;
            _effectFactory = effectFactory;
            _timeProvider = timeProvider;
        }

        public void ExplodeBlocks(Vector2Int gridPosition, ExplosionConfig explosionConfig)
        {
            if (!TryRootBlockProcessing(gridPosition, explosionConfig, out ExplosionInfo explosionInfo))
            {
                return;
            }
            
            CellInfo rootCell = explosionInfo.RootCell;
            List<int> targetIDs = FindTargetBlockIDs(rootCell.BlockPlaceInfo.GridPosition, explosionConfig);
            int maxDepth = CalculateMaxDepth(explosionConfig);
            CalculateExplosionPath(rootCell, explosionConfig, targetIDs, maxDepth);
        }

        public void Restart()
        {
            _explosions.Clear();
        }

        public void Tick()
        {
            if (_explosions.Count == 0)
            {
                return;
            }

            List<ExplosionInfo> explosions = _explosions.Values.ToList();

            for (int i = 0; i < explosions.Count; i++)
            {
                if (!_timeProvider.Stopped)
                {
                    explosions[i].LeftTime -= _timeProvider.DeltaTime;
                }

                if (explosions[i].LeftTime > 0f)
                {
                    continue;
                }
                
                ExplodeWave(explosions[i]);
                explosions[i].LeftTime = explosions[i].ExplosionConfig.SecondsPerWave;
            }
        }
        
        private void CalculateExplosionPath(CellInfo rootCell, ExplosionConfig explosionConfig, List<int> targetIDs, int maxDepth)
        {
            Stack<(CellInfo cell, int depth)> stack = new Stack<(CellInfo cell, int depth)>();
            stack.Push((rootCell, 0));

            while (stack.Count > 0)
            {
                (CellInfo currentCell, int depth) = stack.Pop();
                Vector2Int cellPosition = currentCell.BlockPlaceInfo.GridPosition;

                if (depth >= maxDepth)
                {
                    return;
                }

                foreach (Vector2Int direction in explosionConfig.Directions)
                {
                    Vector2Int newPosition = cellPosition + direction;

                    if (!TryGetCell(targetIDs, newPosition, currentCell, out BlockPlaceInfo blockPlaceInfo))
                    {
                        continue;
                    }

                    CellInfo child = new CellInfo(blockPlaceInfo, currentCell);
                    currentCell.Childrens.Add(child);
                    stack.Push((child, depth + 1));
                }
            }
        }

        private bool TryGetCell(List<int> targetIDs, Vector2Int newNode, CellInfo currentCell, out BlockPlaceInfo blockPlaceInfo)
        {
            if (!_gameGridService.TryGetBlockPlaceInfo(out blockPlaceInfo, newNode))
            {
                return false;
            }

            if (targetIDs.Count != 0 && !targetIDs.Contains(blockPlaceInfo.ID))
            {
                return false;
            }

            if (IsVisited(currentCell, blockPlaceInfo.GridPosition))
            {
                return false;
            }

            return true;
        }

        private bool IsVisited(CellInfo cell, Vector2Int position)
        {
            while (true)
            {
                if (cell.Parent == null)
                {
                    return false;
                }

                if (cell.Parent.BlockPlaceInfo.GridPosition == position)
                {
                    return true;
                }

                cell = cell.Parent;
            }
        }

        private bool TryRootBlockProcessing(Vector2Int gridPosition, ExplosionConfig explosionConfig, out ExplosionInfo explosionInfo)
        {
            explosionInfo = null;
            
            if (!_gameGridService.TryRemoveAt(gridPosition, out BlockPlaceInfo explosionSource))
            {
                Debug.LogWarning("Incorrect source block position");
                return false;
            }

            if (_explosions.ContainsKey(explosionSource))
            {
                Debug.LogWarning("This block is already exploded");
                return false;
            }

            CellInfo cellInfo = new CellInfo(explosionSource, null);
            explosionInfo = new(explosionConfig, cellInfo);
            _explosions[explosionSource] = explosionInfo;
            return true;
        }

        private void ExplodeWave(ExplosionInfo explosionInfo)
        {
            List<CellInfo> childrenCells = new();
            
            foreach (CellInfo cell in explosionInfo.CurrentCells)
            {
                if (explosionInfo.ExplosionConfig.Chain && cell != explosionInfo.RootCell && !explosionInfo.ExplosionConfig.CellIdsToFindTargets.Contains(cell.BlockPlaceInfo.ID))
                {
                    continue;
                }
                CreateEffect(cell.BlockPlaceInfo.WorldPosition, explosionInfo.ExplosionConfig);
                TryExplodeBlock(cell.BlockPlaceInfo.Block, explosionInfo.ExplosionConfig);
                
                childrenCells.AddRange(cell.Childrens);
            }

            explosionInfo.CurrentCells = childrenCells;
        }

        private void TryExplodeBlock(Block block, ExplosionConfig explosionConfig)
        {
            if (block is null)
            {
                return;
            }
            
            if (block.TryGetComponent(out Health health))
            {
                health.TakeDamage(explosionConfig.Damage);
            }
            
            if (block.TryGetComponent(out Explosion explosion))
            {
                explosion.Interact();
            }

            if (block.TryGetComponent(out DestroyOnExplode destroyOnExplode))
            {
                destroyOnExplode.Explode();
            }
            
            if (block.TryGetComponent(out BoostKeeper boostKeeper))
            {
                boostKeeper.Interact();
            }

        }

        private void CreateEffect(Vector2 worldPosition, ExplosionConfig explosionConfig)
        {
            _spawnContext.Position = worldPosition;
            Effect effect = _effectFactory.Spawn(_spawnContext);
            effect.EnableEffect(explosionConfig.ExplosionEffectKey);
        }

        private int CalculateMaxDepth(ExplosionConfig explosionConfig)
        {
            int length = explosionConfig.Length == -1
                ? _gameGridService.GridSize.x * _gameGridService.GridSize.y
                : explosionConfig.Length;
            return length;
        }

        private List<int> FindTargetBlockIDs(Vector2Int gridPosition, ExplosionConfig explosionConfig)
        {
            List<int> targetIDs = new();
            
            if (!explosionConfig.Chain)
            {
                return targetIDs;
            }
            
            Dictionary<int, int> IDs = new();
            foreach (var point in explosionConfig.Directions)
            {
                Vector2Int endPosition = gridPosition + point;

                if (!_gameGridService.TryGetBlockPlaceInfo(out BlockPlaceInfo blockPlaceInfo, endPosition))
                {
                    continue;
                }

                if (!explosionConfig.CellIdsToFindTargets.Contains(blockPlaceInfo.ID))
                {
                    continue;
                }

                IDs.TryAdd(blockPlaceInfo.ID, 0);
                IDs[blockPlaceInfo.ID]++;
            }

            if (IDs.Count != 0)
            {
                targetIDs.Add(FindIDWithMaxCount(IDs)); 
            }

            return targetIDs;
        }

        private static int FindIDWithMaxCount(Dictionary<int, int> IDs)
        {
            return IDs.Aggregate((kvp1, kvp2) => kvp1.Value > kvp2.Value ? kvp1 : kvp2).Key;
        }
    }
}