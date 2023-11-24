using System;
using Main.Scripts.Data;
using Main.Scripts.Logic.Blocks;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.GameGrid
{
    public interface IGameGridService : IService    
    {
        bool TryRemoveAt(Block block, out BlockPlaceInfo blockPlaceInfo);
        bool TryRemoveAt(Vector2Int position, out BlockPlaceInfo blockPlaceInfo);
        bool TryGetBlock(out Block block, Vector2Int gridPosition);
        bool TryGetBlockPlaceInfo(out BlockPlaceInfo blockPlaceInfo, Vector2Int gridPosition);
        bool TryGetWorldPosition(out Vector2 worldPosition, Vector2Int gridPosition);
        bool IsWithinArrayBounds(Vector2Int position);

        event Action OnDestroyed;

        int AllBlocks { get; }
        int AllBlocksToWin { get; }
        int DestroyedBlocksToWin { get; }
        Vector2Int GridSize { get; }
    }
}