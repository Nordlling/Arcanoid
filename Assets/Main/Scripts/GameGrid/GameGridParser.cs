using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Main.Scripts.GameGrid
{
    public class GameGridParser : IGameGridParser
    {
        
        public LevelMapInfo ParseLevelMap(string textData)
        {
            return ParseLevelMapJson(textData);
        }
        
        private LevelMapInfo ParseLevelMapJson(string json)
        {
            LevelMapInfo levelMapInfo = new();
            try
            {
                MapData mapData = JsonConvert.DeserializeObject<MapData>(json);

                if (mapData != null && mapData.layers != null && mapData.layers.Count > 0)
                {
                    levelMapInfo.Width = mapData.width;
                    levelMapInfo.Height = mapData.height;
                    levelMapInfo.LevelMap = new int[levelMapInfo.Width, levelMapInfo.Height];
                    List<int> mapDataList = mapData.layers[0].data;

                    for (int y = 0; y < levelMapInfo.Height; y++)
                    {
                        for (int x = 0; x < levelMapInfo.Width; x++)
                        {
                            int tileValue = mapDataList[y * levelMapInfo.Width + x];
                            levelMapInfo.LevelMap[x, y] = tileValue;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogWarning("Error parsing JSON: " + e.Message);
            }

            return levelMapInfo;
        }
        
        [Serializable]
        private class MapData
        {
            public int width;
            public int height;
            public List<Layer> layers;

            [Serializable]
            public class Layer
            {
                public List<int> data;
            }
        }
    }
}