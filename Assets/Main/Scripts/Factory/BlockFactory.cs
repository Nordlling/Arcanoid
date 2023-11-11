using System;
using Main.Scripts.Configs;
using Main.Scripts.Factory.Components;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Logic.Blocks;
using Main.Scripts.Pool;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Main.Scripts.Factory
{
    public class BlockFactory : IBlockFactory
    {
        private readonly ServiceContainer _serviceContainer;
        private readonly TiledBlockConfig _tiledBlockConfig;
        private readonly PoolProvider _poolProvider;

        private Component[] _basicComponents;

        public BlockFactory(ServiceContainer serviceContainer, TiledBlockConfig tiledBlockConfig, PoolProvider poolProvider)
        {
            _serviceContainer = serviceContainer;
            _tiledBlockConfig = tiledBlockConfig;
            _poolProvider = poolProvider;
        }

        public Block Spawn(SpawnContext spawnContext)
        {
            if (!_tiledBlockConfig.BlockInfos.ContainsKey(spawnContext.ID))
            {
                return null;
            }
            
            Block block = (Block)_poolProvider.PoolItemView.Spawn();
            
            _basicComponents ??= block.GetComponents<Component>();
            
            block.Construct(this, spawnContext.ID);
            block.transform.position = spawnContext.Position;
            block.SpriteRenderer.sprite = _tiledBlockConfig.BlockInfos[spawnContext.ID].Visual;
            block.Collider.size = block.SpriteRenderer.bounds.size;
            
            IComponentFactory[] componentFactories = _tiledBlockConfig.BlockInfos[spawnContext.ID].ComponentFactories;
            
            foreach (IComponentFactory componentFactory in componentFactories)
            {
                componentFactory.AddComponent(_serviceContainer, block, spawnContext);
            }
            
            return block;
        }

        public void Despawn(Block block)
        {
            foreach (Component component in block.GetComponents<Component>())
            {
                if (Array.Exists(_basicComponents, c => c != null && c.GetType() == component.GetType()))
                {
                    continue;
                }
                Object.Destroy(component);
            }
            
            _poolProvider.PoolItemView.Despawn(block);
        }
    }
}