using System;
using Main.Scripts.Data;
using Main.Scripts.Logic.Blocks;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.GameGrid
{
    public interface IGameGridService : IService    
    {
        CurrentLevelInfo CurrentLevelInfo { get; set; }
        void CreateLevelMap();
        void RemoveAt(Block block);
        void RemoveAt(Vector2Int position);
        bool TryGet(out Block block, Vector2Int position);
        void ResetCurrentLevel();
        event Action OnDestroyed;
        int AllBlocks { get; }
        int AllBlocksToWin { get; }
        int DestroyedBlocksToWin { get; }
        Vector2Int GridSize { get; }
    }
}