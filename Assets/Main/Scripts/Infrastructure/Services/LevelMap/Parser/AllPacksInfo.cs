using System;
using System.Collections.Generic;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.LevelMap.Parser
{
    [Serializable]
    public class AllPacksInfo
    {
        public List<PackConfig> PackConfigs;
    }
    
    [Serializable]
    public class PackConfig
    {
        public string PackID;
        public string PackInfoPath;
    }
    
    
    [Serializable]
    public class PackInfo
    {
        public string PackID;
        public string PackName;
        public string MapImagePath;
        public Sprite MapImage;
        public string ButtonColor;
        public int LevelsCount;
        public string LevelsPath;
        public List<PackLevelInfo> Levels;
    }

    [Serializable]
    public class PackLevelInfo
    {
        public string LevelID;
        public string FileName;
    }

}