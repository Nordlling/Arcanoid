using System;
using Main.Scripts.Data;
using Main.Scripts.Logic.Blocks;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.GameGrid
{
    public interface IGameGridService : IService    
    {
        void CreateLevelMap();
        void RemoveAt(Block block);
        void RemoveAt(Vector2Int position);
        bool TryGetBlock(out Block block, Vector2Int gridPosition);
        bool TryGetWorldPosition(out Vector2 worldPosition, Vector2Int gridPosition);
        bool IsWithinArrayBounds(Vector2Int position);

        event Action OnDestroyed;

        CurrentLevelInfo CurrentLevelInfo { get; set; }
        int AllBlocks { get; }
        int AllBlocksToWin { get; }
        int DestroyedBlocksToWin { get; }
        Vector2Int GridSize { get; }
    }
}