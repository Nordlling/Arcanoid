using Main.Scripts.Configs.Boosts;
using Main.Scripts.Infrastructure.Services.Collision;
using Main.Scripts.Logic.Explosions;
using UnityEngine;

namespace Main.Scripts.Logic.Blocks
{
    public class Explosion : MonoBehaviour, ICollisionInteractable
    {
        private Block _block;
        private ExplosionConfig _explosionConfig;
        private ExplosionSystem _explosionSystem;

        public void Construct(Block block, ExplosionConfig explosionConfig, ExplosionSystem explosionSystem)
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