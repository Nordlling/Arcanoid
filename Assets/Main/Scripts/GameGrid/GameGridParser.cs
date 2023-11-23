using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Main.Scripts.GameGrid
{
    public class GameGridParser : IGameGridParser
    {
        public GameGridInfo ParseLevelMap(string textData)
        {
            return ParseLevelMapJson(textData);
        }
        
        private GameGridInfo ParseLevelMapJson(string json)
        {
            GameGridInfo gameGridInfo = new();
            try
            {
                TiledMapData tiledMapData = JsonConvert.DeserializeObject<TiledMapData>(json);

                if (tiledMapData != null && tiledMapData.layers != null && tiledMapData.layers.Count > 0)
                {
                    gameGridInfo.Size.x = tiledMapData.width;
                    gameGridInfo.Size.y = tiledMapData.height;
                    gameGridInfo.LevelMap = new int[gameGridInfo.Size.x, gameGridInfo.Size.y];
                    List<int> mapDataList = tiledMapData.layers[0].data;

                    for (int y = 0; y < gameGridInfo.Size.y; y++)
                    {
                        for (int x = 0; x < gameGridInfo.Size.x; x++)
                        {
                            int tileValue = mapDataList[y * gameGridInfo.Size.x + x];
                            gameGridInfo.LevelMap[x, y] = tileValue;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogWarning("Error parsing JSON: " + e.Message);
            }

            return gameGridInfo;
        }
        
        [Serializable]
        private class TiledMapData
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