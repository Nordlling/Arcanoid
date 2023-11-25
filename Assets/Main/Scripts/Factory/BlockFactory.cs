using Main.Scripts.Configs;
using Main.Scripts.Factory.Components;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Infrastructure.Services.GameGrid;
using Main.Scripts.Logic.Blocks;
using Main.Scripts.Pool;

namespace Main.Scripts.Factory
{
    public class BlockFactory : IBlockFactory
    {
        private readonly ServiceContainer _serviceContainer;
        private readonly TiledBlockConfig _tiledBlockConfig;
        private readonly PoolProvider _poolProvider;

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

            BlockInfo blockInfo = _tiledBlockConfig.BlockInfos[spawnContext.ID];
            Block block = (Block)_poolProvider.PoolItemView.Spawn();
            
            block.Construct(_serviceContainer.Get<IGameGridService>(), spawnContext.ID, blockInfo.CheckToWin);
            block.transform.position = spawnContext.Position;
            block.SpriteRenderer.sprite = blockInfo.BasicInfo.Visual;
            block.Collider.size = block.SpriteRenderer.size;
            
            IBlockComponentFactory[] componentFactories = blockInfo.ComponentFactories;
            
            foreach (IBlockComponentFactory componentFactory in componentFactories)
            {
                componentFactory.AddComponent(_serviceContainer, block, spawnContext);
            }
            
            return block;
        }

        public void Despawn(Block block)
        {
            IBlockComponentFactory[] componentFactories = _tiledBlockConfig.BlockInfos[block.ID].ComponentFactories;
            
            foreach (IBlockComponentFactory componentFactory in componentFactories)
            {
                componentFactory.RemoveComponent(block);
            }
            
            _poolProvider.PoolItemView.Despawn(block);
        }
    }
}