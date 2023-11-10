using Main.Scripts.Configs;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Logic.Blocks;
using Unity.VisualScripting;

namespace Main.Scripts.Factory.Components
{
    public class BreakVisualComponentFactory : IComponentFactory
    {
        public BlockBreaksConfig BlockBreaksConfig;
        
        public void AddComponent(ServiceContainer serviceContainer, Block block, BlockSpawnContext spawnContext)
        {
            BreaksVisual breaksVisual = block.AddComponent<BreaksVisual>();
            breaksVisual.Construct(block.BreakSpriteRenderer, BlockBreaksConfig.BreakSprites);
        }
    }
}