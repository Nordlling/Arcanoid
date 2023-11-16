using System.Collections.Generic;
using Main.Scripts.Infrastructure.Services.LevelMap.Parser;

namespace Main.Scripts.Infrastructure.Services.LevelMap
{
    public interface IPackService : IService
    {
        List<PackProgress> PackProgresses { get; }
        List<PackInfo> PackInfos { get; }
        int SelectedPackIndex { get; set; }
        int WonPackIndex { get; set; }
        string GetCurrentLevelPath();
        void LevelUp();
    
    }
}