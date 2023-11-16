using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Logic.Blocks;
using Unity.VisualScripting;
using Object = UnityEngine.Object;

namespace Main.Scripts.Factory.Components
{
    public class HealthComponentFactory : IComponentFactory
    {
        public int HealthCount;

        public void AddComponent(ServiceContainer serviceContainer, Block block, SpawnContext spawnContext)
        {
            Health health = block.AddComponent<Health>();
            health.Construct(HealthCount);
            block.Subscribe(health);
        }
        
        public void RemoveComponent(Block block)
        {
            if (block.TryGetComponent(out Health health))
            {
                Object.Destroy(health);
            }
        }
    }
}