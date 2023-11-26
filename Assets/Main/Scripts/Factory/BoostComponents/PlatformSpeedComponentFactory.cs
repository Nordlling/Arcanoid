using Main.Scripts.Configs.Boosts;
using Main.Scripts.Factory.Components;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Logic.Boosts;
using Main.Scripts.Logic.Platforms.PlatformBoosts;
using Main.Scripts.Logic.Platforms.PlatformSystems;
using Main.Scripts.Pool;
using Unity.VisualScripting;
using Object = UnityEngine.Object;

namespace Main.Scripts.Factory.BoostComponents
{
    public class PlatformSpeedComponentFactory : IBoostComponentFactory
    {   
        public SpeedPlatformConfig SpeedPlatformConfig;
        
        public void AddComponent<T>(ServiceContainer serviceContainer, T unit, SpawnContext spawnContext) where T : SpawnableItemMono
        {
            if (unit is not Boost boost)
            {
                return;
            }
            PlatformBoostSpeed platformBoostSpeed = boost.AddComponent<PlatformBoostSpeed>();
            platformBoostSpeed.Construct(boost, SpeedPlatformConfig, serviceContainer.Get<ISpeedPlatformSystem>());
        }
        
        public void RemoveComponent<T>(T unit) where T : SpawnableItemMono
        {
            if (unit.TryGetComponent(out PlatformBoostSpeed platformBoostSpeed))
            {
                Object.Destroy(platformBoostSpeed);
            }
        }
    }
}