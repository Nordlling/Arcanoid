using System.Collections.Generic;
using System.Linq;
using Main.Scripts.Configs;
using Main.Scripts.Logic.Blocks;
using Main.Scripts.Pool;
using Unity.VisualScripting;
using UnityEngine;

namespace Main.Scripts.Factory
{
    public class BombBlockFactory : AbstractBlockFactory
    {
        [SerializeField] private PoolProvider _poolProvider;
        
        private Dictionary<int, BlockInfo> _tiledBlockDictionary;

        public void Construct(TiledBlockConfig tiledBlockConfig)
        {
            _tiledBlockDictionary = tiledBlockConfig.BlockInfos.ToDictionary(key => key.BlockID, value => value);
        }
          
        public override Block Spawn(BlockSpawnContext spawnContext)
        {
            if (!_tiledBlockDictionary.ContainsKey(spawnContext.BlockID))
            {
                return null;
            }
            
            Block block = (Block)_poolProvider.PoolViewBlock.Spawn();
            block.Construct(this);
            
            Explosion explosion = block.AddComponent<Explosion>();
            
            block.transform.position = spawnContext.SpawnPosition;
            block.SpriteRenderer.sprite = _tiledBlockDictionary[spawnContext.BlockID].Visual;
            block.Collider.size = block.SpriteRenderer.bounds.size;
            block.Health.Construct(_tiledBlockDictionary[spawnContext.BlockID].HealthCount, 0);
            return block;
        }

        public override void Despawn(Block block)
        {
            _poolProvider.PoolViewBlock.Despawn(block);
        }
    }
}