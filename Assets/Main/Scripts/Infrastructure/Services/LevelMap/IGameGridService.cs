using Main.Scripts.Data;
using Main.Scripts.Logic.Blocks;

namespace Main.Scripts.Infrastructure.Services.LevelMap
{
    public interface IGameGridService : IService    
    {
        CurrentLevelInfo CurrentLevelInfo { get; set; }
        void CreateLevelMap();
        void RemoveBlockFromGrid(Block block);
        void ResetCurrentLevel();
    }
}