using System.Collections.Generic;
using System.Linq;
using Main.Scripts.Configs;
using Main.Scripts.Pool;
using UnityEngine;

namespace Main.Scripts.Factory
{
    public class BlockFactoryUnit : AbstractFactoryUnit
    {
        private readonly ObjectPoolMono<SpawnableItemMono> _objectPoolMono;
        private readonly Dictionary<int, BlockInfo> _tiledBlockDictionary;

        public BlockFactoryUnit(ObjectPoolMono<SpawnableItemMono> objectPoolMono, TiledBlockConfig tiledBlockConfig)
        {
            _objectPoolMono = objectPoolMono;
            _tiledBlockDictionary = tiledBlockConfig.BlockInfos.ToDictionary(key => key.BlockID, value => value);
        }
          
        public override GameObject Spawn(UnitSpawnContext spawnContext)
        {
            // block.transform.parent = null;
            if (spawnContext is not BlockSpawnContext blockSpawnContext)
            {
                return null;
            }
            if (!_tiledBlockDictionary.ContainsKey(blockSpawnContext.BlockID))
            {
                return null;
            }
            SpawnableItemMono block = _objectPoolMono.Spawn();
            
            block.transform.position = blockSpawnContext.SpawnPosition;
            block.GetComponent<SpriteRenderer>().sprite = _tiledBlockDictionary[blockSpawnContext.BlockID].Visual;
            return block.gameObject;
        }

        public override void Despawn(GameObject gameObject)
        {
            SpawnableItemMono block = gameObject.GetComponent<SpawnableItemMono>();
            _objectPoolMono.Despawn(block);
        }
    }
}