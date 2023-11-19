using System;
using System.Collections.Generic;

namespace Main.Scripts.Infrastructure.Services.Packs
{
    public interface IPackService : IService
    {
        List<PackProgress> PackProgresses { get; }
        List<PackInfo> PackInfos { get; }
        int SelectedPackIndex { get; set; }
        int WonPackIndex { get; }
        int WonLevelIndex { get; }
        string GetCurrentLevelPath();
        void LevelUp();

        event Action OnLevelUp;
    }
}