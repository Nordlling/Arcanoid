using System.Collections.Generic;
using Main.Scripts.Configs.Boosts;
using Main.Scripts.Infrastructure.Services.Collision;
using Main.Scripts.Infrastructure.Services.GameGrid;
using UnityEngine;

namespace Main.Scripts.Logic.Blocks
{
    public class Explosion : MonoBehaviour, ICollisionInteractable
    {
        private IGameGridService _gameGridService;
        private Block _block;
        private ExplosionConfig _explosionConfig;

        private List<ICollisionInteractable> _interactables;


        private readonly Vector2Int[] _points =
        {
            new(1, 0),
            new(0, -1),
            new(-1, 0),
            new(0, 1),
            new(1, 1),
            new(1, -1),
            new(-1, -1),
            new(-1, 1)
        };

        public void Construct(IGameGridService gameGridService, Block block, ExplosionConfig explosionConfig)
        {
            _gameGridService = gameGridService;
            _block = block;
            _explosionConfig = explosionConfig;
        }

        public void Interact()
        {
            _gameGridService.RemoveAt(_block.GridPosition);
            
            foreach (var point in _points)
            {
                if (_gameGridService.TryGet(out Block block ,_block.GridPosition + point))
                {
                    TryExplode(block);
                }
            }
        }

        private void TryExplode(Block block)
        {
            if (block.TryGetComponent(out Health health))
            {
                health.TakeDamage(_explosionConfig.Damage);
            }
            
            if (block.TryGetComponent(out Explosion explosion))
            {
                explosion.Interact();
            }

            if (block.TryGetComponent(out DestroyOnExplode destroyOnExplode))
            {
                destroyOnExplode.Explode();
            }

        }
    }
}