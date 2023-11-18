using System;
using System.Collections.Generic;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.Packs
{
    [Serializable]
    public class AllPacksData
    {
        public List<PackData> PackConfigs;
    }
    
    [Serializable]
    public class PackData
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