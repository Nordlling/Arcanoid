using System;
using Main.Scripts.Data;
using Main.Scripts.Logic.Blocks;

namespace Main.Scripts.Infrastructure.Services.GameGrid
{
    public interface IGameGridService : IService    
    {
        CurrentLevelInfo CurrentLevelInfo { get; set; }
        void CreateLevelMap();
        void RemoveBlockFromGrid(Block block);
        void ResetCurrentLevel();
        event Action OnDestroyed;
        int AllBlocks { get; }
        int AllBlocksToWin { get; }
        int DestroyedBlocksToWin { get; }
    }
}