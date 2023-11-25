using Main.Scripts.Configs.Boosts;
using Main.Scripts.Factory.Components;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Logic.Blocks;
using Main.Scripts.Logic.Explosions;
using Main.Scripts.Pool;
using Unity.VisualScripting;
using Object = UnityEngine.Object;

namespace Main.Scripts.Factory.BlockComponents
{
    public class ExplosionComponentFactory : IBlockComponentFactory
    {
        public ExplosionConfig ExplosionConfig;
        
        public void AddComponent<T>(ServiceContainer serviceContainer, T unit, SpawnContext spawnContext) where T : SpawnableItemMono
        {
            if (unit is not Block block)
            {
                return;
            }
            Explosion explosion = block.AddComponent<Explosion>();
            explosion.Construct(block, ExplosionConfig, serviceContainer.Get<IExplosionSystem>());
        }
        
        public void RemoveComponent<T>(T unit) where T : SpawnableItemMono
        {
            if (unit.TryGetComponent(out Explosion explosion))
            {
                Object.Destroy(explosion);
            }
        }
    }
}