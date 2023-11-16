using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Logic.Blocks;
using Unity.VisualScripting;
using Object = UnityEngine.Object;

namespace Main.Scripts.Factory.Components
{
    public class ExplosionComponentFactory : IComponentFactory
    {
        public void AddComponent(ServiceContainer serviceContainer, Block block, SpawnContext spawnContext)
        {
            Explosion explosion = block.AddComponent<Explosion>();
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