using Main.Scripts.Data;
using UnityEngine;

namespace Main.Scripts.LevelMap
{
    public class GameGridLoader : ILevelMapLoader
    {
        public string LoadLevelMap(string path, CurrentLevelInfo currentLevelInfo)
        {
            return Resources.Load<TextAsset>(string.Format(path, currentLevelInfo.CurrentPack, currentLevelInfo.CurrentLevel)).text;
        }
        
    }
}