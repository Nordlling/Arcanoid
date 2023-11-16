using System;
using UnityEngine;

namespace Main.Scripts.GameGrid
{
    public class GameGridLoader : IGameGridLoader
    {
        public string LoadLevelMap(string path)
        {
            try
            {
                return Resources.Load<TextAsset>(path).text;
            }
            catch (Exception e)
            {
                Debug.LogWarning($"Error load file: {e.Message}");
            }

            return null;
        }
        
    }
}