using Main.Scripts.Configs;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Logic.Blocks;
using Unity.VisualScripting;
using Object = UnityEngine.Object;

namespace Main.Scripts.Factory.Components
{
    public class BreakVisualComponentFactory : IComponentFactory
    {
        public BlockBreaksConfig BlockBreaksConfig;
        
        public void AddComponent(ServiceContainer serviceContainer, Block block, SpawnContext spawnContext)
        {
            BreaksVisual breaksVisual = block.AddComponent<BreaksVisual>();
            breaksVisual.Construct(block.BreakSpriteRenderer, BlockBreaksConfig.BreakSprites);
        }

        public void RemoveComponent(Block block)
        {
            if (block.TryGetComponent(out BreaksVisual breaksVisual))
            {
                Object.Destroy(breaksVisual);
            }
        }
    }
}