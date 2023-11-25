using Main.Scripts.Configs;
using Main.Scripts.Factory.Components;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Logic.Blocks;
using Main.Scripts.Pool;
using Unity.VisualScripting;
using Object = UnityEngine.Object;

namespace Main.Scripts.Factory.BlockComponents
{
    public class BlockHealthVisualComponentFactory : IBlockComponentFactory
    {
        public BlockHealthVisualConfig BlockHealthVisualConfig;
        
        public void AddComponent<T>(ServiceContainer serviceContainer, T unit, SpawnContext spawnContext) where T : SpawnableItemMono
        {
            if (unit is not Block block)
            {
                return;
            }
            HealthVisual healthVisual = unit.AddComponent<HealthVisual>();
            healthVisual.Construct(serviceContainer.Get<IEffectFactory>(), block.BreakSpriteRenderer, BlockHealthVisualConfig);
        }

        public void RemoveComponent<T>(T unit) where T : SpawnableItemMono
        {
            if (unit.TryGetComponent(out HealthVisual breaksVisual))
            {
                Object.Destroy(breaksVisual);
            }
        }
    }
}