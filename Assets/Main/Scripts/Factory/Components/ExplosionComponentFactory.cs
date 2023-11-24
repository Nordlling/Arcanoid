using Main.Scripts.Configs.Boosts;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Logic.Blocks;
using Main.Scripts.Logic.Explosions;
using Unity.VisualScripting;
using Object = UnityEngine.Object;

namespace Main.Scripts.Factory.Components
{
    public class ExplosionComponentFactory : IComponentFactory
    {
        public ExplosionConfig ExplosionConfig;
        
        public void AddComponent(ServiceContainer serviceContainer, Block block, SpawnContext spawnContext)
        {
            Explosion explosion = block.AddComponent<Explosion>();
            explosion.Construct(block, ExplosionConfig, serviceContainer.Get<ExplosionSystem>());
        }
        
        public void RemoveComponent(Block block)
        {
            if (block.TryGetComponent(out Explosion explosion))
            {
                Object.Destroy(explosion);
            }
        }
    }
}