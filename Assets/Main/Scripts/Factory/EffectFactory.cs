using Main.Scripts.Infrastructure.Provides;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Logic.Effects;
using Main.Scripts.Pool;

namespace Main.Scripts.Factory
{
    public class EffectFactory : IEffectFactory
    {
        private readonly ServiceContainer _serviceContainer;
        private readonly PoolProvider _poolProvider;

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
            return effect;
        }

        public void Despawn(Effect effect)
        {
            _poolProvider.PoolItemView.Despawn(effect);
        }
    }

    public interface IEffectFactory
    {
        Effect Spawn(SpawnContext spawnContext);
        void Despawn(Effect effect);
    }
}