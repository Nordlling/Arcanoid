using Main.Scripts.Infrastructure.Provides;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Logic.Effects;
using Main.Scripts.Pool;
using UnityEngine;

namespace Main.Scripts.Factory
{
    public class EffectFactory : IEffectFactory
    {
        private readonly ServiceContainer _serviceContainer;
        private readonly PoolProvider _poolProvider;

        private readonly SpawnContext _spawnContext = new();

        public EffectFactory(ServiceContainer serviceContainer, PoolProvider poolProvider)
        {
            _serviceContainer = serviceContainer;
            _poolProvider = poolProvider;
        }

        public Effect Spawn(SpawnContext spawnContext)
        {
            Effect effect = (Effect)_poolProvider.PoolItemView.Spawn();
            effect.Construct(this, _serviceContainer.Get<ITimeProvider>());
            effect.transform.position = spawnContext.Position;
            effect.transform.localScale = spawnContext.Scale;
            return effect;
        }

        public Effect SpawnAndEnable(Vector2 position, string effectKey)
        {
            return SpawnAndEnable(position, Vector3.one, effectKey);
        }

        public Effect SpawnAndEnable(Vector2 position, Vector3 scale, string effectKey)
        {
            if (string.IsNullOrEmpty(effectKey))
            {
                return null;
            }
            
            _spawnContext.Reset();
            _spawnContext.Position = position;
            _spawnContext.Scale = scale;
            Effect effect = Spawn(_spawnContext);
            effect.EnableEffect(effectKey);
            return effect;
        }

        public void Despawn(Effect effect)
        {
            _poolProvider.PoolItemView.Despawn(effect);
        }
    }
}