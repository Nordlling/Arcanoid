using Main.Scripts.Configs.Boosts;
using Main.Scripts.Infrastructure.Services.Collision;
using Main.Scripts.Logic.Explosions;
using UnityEngine;

namespace Main.Scripts.Logic.Blocks
{
    public class Explosion : MonoBehaviour, ICollisionInteractable, ITriggerInteractable
    {
        private Block _block;
        private ExplosionConfig _explosionConfig;
        private IExplosionSystem _explosionSystem;

        public void Construct(Block block, ExplosionConfig explosionConfig, IExplosionSystem explosionSystem)
        {
            _block = block;
            _explosionConfig = explosionConfig;
            _explosionSystem = explosionSystem;
        }

        public void Interact()
        {
            _explosionSystem.ExplodeBlocks(_block.GridPosition, _explosionConfig);
        }
    }
}