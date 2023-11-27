using Main.Scripts.Configs.Boosts;
using Main.Scripts.Logic.Blocks;
using Main.Scripts.Logic.Boosts;

namespace Main.Scripts.Logic.Explosions
{
    public class ExplosionInteractionProcessor
    {
        public void ExplodedBlockProcessing(Block block, ExplosionConfig explosionConfig)
        {
            if (block is null)
            {
                return;
            }
            
            if (block.TryGetComponent(out Health health))
            {
                health.TakeDamage(explosionConfig.Damage);
            }
            
            if (block.TryGetComponent(out Explosion explosion))
            {
                explosion.Interact();
            }

            if (block.TryGetComponent(out DestroyOnExplode destroyOnExplode))
            {
                destroyOnExplode.Explode();
            }
            
            if (block.TryGetComponent(out BoostKeeper boostKeeper))
            {
                boostKeeper.Interact();
            }
            
            if (block.TryGetComponent(out ExtraBall extraBall))
            {
                extraBall.Interact();
            }
        }
    }
    
}