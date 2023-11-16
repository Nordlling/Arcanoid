using System;
using System.Collections.Generic;

namespace Main.Scripts.Infrastructure.Services.LevelMap
{
    [Serializable]
    public class PacksProgress
    {
        public List<PackProgress> Packs;
    }
    
    [Serializable]
    public class PackProgress
    {
        public string PackID;
        public int CurrentLevelIndex;
        public bool IsOpen;
        public bool IsPassed;
    }
}