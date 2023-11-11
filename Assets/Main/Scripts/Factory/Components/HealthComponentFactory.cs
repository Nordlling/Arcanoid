using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Logic.Blocks;
using Unity.VisualScripting;

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
    }
}