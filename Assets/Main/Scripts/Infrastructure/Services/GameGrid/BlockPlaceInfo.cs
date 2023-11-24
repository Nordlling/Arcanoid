using Main.Scripts.Logic.Blocks;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.GameGrid
{
    public class BlockPlaceInfo
    {
        public int ID;
        public Block Block;
        public bool CheckToWin;
        public Vector2Int GridPosition;
        public Vector2 WorldPosition;

        public BlockPlaceInfo(int id, Block block, bool checkToWin, Vector2Int gridPosition, Vector2 worldPosition)
        {
            ID = id;
            Block = block;
            CheckToWin = checkToWin;
            GridPosition = gridPosition;
            WorldPosition = worldPosition;
        }
        
    }
}