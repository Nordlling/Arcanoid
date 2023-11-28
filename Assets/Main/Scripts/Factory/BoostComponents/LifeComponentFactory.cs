using Main.Scripts.Configs.Boosts;
using Main.Scripts.Factory.Components;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Infrastructure.Services.Healths;
using Main.Scripts.Logic.Boosts;
using Main.Scripts.Pool;
using Unity.VisualScripting;
using Object = UnityEngine.Object;

namespace Main.Scripts.Factory.BoostComponents
{
    public class LifeComponentFactory : IBoostComponentFactory
    {   
        public LifeConfig LifeConfig;
        
        public void AddComponent<T>(ServiceContainer serviceContainer, T unit, SpawnContext spawnContext) where T : SpawnableItemMono
        {
            if (unit is not Boost boost)
            {
                return;
            }
            LifeBoost lifeBoost = boost.AddComponent<LifeBoost>();
            lifeBoost.Construct(
                boost, 
                LifeConfig, 
                serviceContainer.Get<ILifeSystem>()
                );
        }
        
        public void RemoveComponent<T>(T unit) where T : SpawnableItemMono
        {
            if (unit.TryGetComponent(out LifeBoost lifeBoost))
            {
                Object.Destroy(lifeBoost);
            }
        }
        
    }
}