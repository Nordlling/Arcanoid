using Main.Scripts.Logic.Blocks;

namespace Main.Scripts.Infrastructure.Services.LevelMap
{
    public class BlockPlaceInfo
    {
        public int ID;
        public Block Block;
        public bool CheckToWin;
        
        public BlockPlaceInfo(int id, Block block, bool checkToWin)
        {
            ID = id;
            Block = block;
            CheckToWin = checkToWin;
        }
        
    }
}