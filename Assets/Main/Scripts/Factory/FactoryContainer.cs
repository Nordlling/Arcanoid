using System.Collections.Generic;
using System.Linq;
using Main.Scripts.Configs;

namespace Main.Scripts.Factory
{
    public class FactoryContainer : IFactoryContainer
    {
        private Dictionary<int, IBlockFactory> _factoryClassIdDict { get; } = new();
        private Dictionary<int, int> _blockIdDict;

        public FactoryContainer(TiledBlockConfig tiledBlockConfig)
        {
            _blockIdDict = tiledBlockConfig.BlockInfos.ToDictionary(key => key.BlockID, value => value.ClassID);
        }

        public void AddFactory(int classId, IBlockFactory blockFactory)
        {
            _factoryClassIdDict[classId] = blockFactory;
        }

        public IBlockFactory GetFactoryByClassId(int classId)
        {
            return _factoryClassIdDict[classId];
        }
        
        public IBlockFactory GetFactoryByBlockId(int blockId)
        {
            return _factoryClassIdDict[_blockIdDict[blockId]];
        }
        
    }

    public interface IFactoryContainer
    {
        void AddFactory(int classId, IBlockFactory blockFactory);
        IBlockFactory GetFactoryByClassId(int classId);
        IBlockFactory GetFactoryByBlockId(int blockId);
    }
}