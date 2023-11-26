using Main.Scripts.Configs.Boosts;
using Main.Scripts.Factory.Components;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Logic.Balls.BallBoosts;
using Main.Scripts.Logic.Balls.BallSystems;
using Main.Scripts.Logic.Boosts;
using Main.Scripts.Pool;
using Unity.VisualScripting;
using Object = UnityEngine.Object;

namespace Main.Scripts.Factory.BoostComponents
{
    public class FireballBoostComponentFactory : IBoostComponentFactory
    {   
        public FireballConfig FireballConfig;
        
        public void AddComponent<T>(ServiceContainer serviceContainer, T unit, SpawnContext spawnContext) where T : SpawnableItemMono
        {
            if (unit is not Boost boost)
            {
                return;
            }
            FireballBoost fireballBoost = boost.AddComponent<FireballBoost>();
            fireballBoost.Construct(boost, FireballConfig, serviceContainer.Get<IFireballSystem>());
        }
        
        public void RemoveComponent<T>(T unit) where T : SpawnableItemMono
        {
            if (unit.TryGetComponent(out FireballBoost fireballBoost))
            {
                Object.Destroy(fireballBoost);
            }
        }
        
    }
}