using Main.Scripts.Configs.Boosts;
using Main.Scripts.Factory.Components;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Logic.Boosts;
using Main.Scripts.Logic.Platforms;
using Main.Scripts.Pool;
using Unity.VisualScripting;
using Object = UnityEngine.Object;

namespace Main.Scripts.Factory.BoostComponents
{
    public class PlatformSizeComponentFactory : IBoostComponentFactory
    {   
        public SizePlatformConfig SizePlatformConfig;
        
        public void AddComponent<T>(ServiceContainer serviceContainer, T unit, SpawnContext spawnContext) where T : SpawnableItemMono
        {
            if (unit is not Boost boost)
            {
                return;
            }
            PlatformBoostSize ballBoostSpeed = boost.AddComponent<PlatformBoostSize>();
            ballBoostSpeed.Construct(boost, SizePlatformConfig, serviceContainer.Get<ISizePlatformSystem>());
        }
        
        public void RemoveComponent<T>(T unit) where T : SpawnableItemMono
        {
            if (unit.TryGetComponent(out PlatformBoostSize platformBoostSize))
            {
                Object.Destroy(platformBoostSize);
            }
        }
    }
}