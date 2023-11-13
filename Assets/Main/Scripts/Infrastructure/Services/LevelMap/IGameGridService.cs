using Main.Scripts.Data;

namespace Main.Scripts.Infrastructure.Services.LevelMap
{
    public interface IGameGridService : IService    
    {
        CurrentLevelInfo CurrentLevelInfo { get; set; }
        void CreateLevelMap();
    }
}