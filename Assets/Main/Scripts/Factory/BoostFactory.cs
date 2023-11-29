using Main.Scripts.Configs;
using Main.Scripts.Factory.Components;
using Main.Scripts.Infrastructure.Provides;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Infrastructure.Services.Collision;
using Main.Scripts.Logic.Boosts;
using Main.Scripts.Pool;
using UnityEngine;

namespace Main.Scripts.Factory
{
    public class BoostFactory : IBoostFactory
    {
        private readonly ServiceContainer _serviceContainer;
        private readonly TiledBlockConfig _tiledBlockConfig;
        private readonly PoolProvider _poolProvider;

        public BoostFactory(ServiceContainer serviceContainer, TiledBlockConfig tiledBlockConfig, PoolProvider poolProvider)
        {
            _serviceContainer = serviceContainer;
            _tiledBlockConfig = tiledBlockConfig;
            _poolProvider = poolProvider;
        }

        public Boost Spawn(SpawnContext spawnContext)
        {
            if (!_tiledBlockConfig.BoostInfos.ContainsKey(spawnContext.ID))
            {
                return null;
            }

            BoostInfo boostInfo = _tiledBlockConfig.BoostInfos[spawnContext.ID];
            
            Boost boost = (Boost)_poolProvider.PoolItemView.Spawn();
            boost.Construct(
                spawnContext.ID, 
                _serviceContainer.Get<IBoostContainer>(), 
                _serviceContainer.Get<IEffectFactory>(), 
                boostInfo.BasicInfo.DestroyEffectKey);
            
            boost.SpriteRenderer.sprite = boostInfo.BasicInfo.Visual;
            boost.CollisionDetector.Construct(_serviceContainer.Get<IBoostCollisionService>());
            boost.transform.position = spawnContext.Position;
            boost.transform.localScale = Vector3.one;
            boost.BoostMovement.Construct(_serviceContainer.Get<ITimeProvider>());

            IBoostComponentFactory[] componentFactories = boostInfo.ComponentFactories;
            
            if (componentFactories is not null)
            {
                foreach (IBoostComponentFactory componentFactory in componentFactories)
                {
                    componentFactory.AddComponent(_serviceContainer, boost, spawnContext);
                }
            }
            
            return boost;
        }

        public void Despawn(Boost boost)
        {
            IBoostComponentFactory[] componentFactories = _tiledBlockConfig.BoostInfos[boost.ID].ComponentFactories;
            
            foreach (IBoostComponentFactory componentFactory in componentFactories)
            {
                componentFactory.RemoveComponent(boost);
            }
            
            _poolProvider.PoolItemView.Despawn(boost);
        }
    }
}