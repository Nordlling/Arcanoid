using Main.Scripts.Factory.Components;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Logic.Balls;
using Main.Scripts.Pool;
using Unity.VisualScripting;
using Object = UnityEngine.Object;

namespace Main.Scripts.Factory.BallComponents
{
    public class HitEffectComponentFactory : IBallComponentFactory
    {
        public string HitEffectKey;
        
        public void AddComponent<T>(ServiceContainer serviceContainer, T unit, SpawnContext spawnContext) where T : SpawnableItemMono
        {
            HitEffect hitEffect = unit.AddComponent<HitEffect>();
            hitEffect.Construct(serviceContainer.Get<IEffectFactory>(), HitEffectKey);
        }
        
        public void RemoveComponent<T>(T unit) where T : SpawnableItemMono
        {
            if (unit.TryGetComponent(out HitEffect hitEffect))
            {
                Object.Destroy(hitEffect);
            }
        }
    }
}