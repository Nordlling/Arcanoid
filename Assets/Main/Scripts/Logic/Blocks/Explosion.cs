using System.Threading.Tasks;
using Main.Scripts.Configs.Boosts;
using Main.Scripts.Factory;
using Main.Scripts.Infrastructure.Services.Collision;
using Main.Scripts.Infrastructure.Services.GameGrid;
using Main.Scripts.Logic.Effects;
using UnityEngine;

namespace Main.Scripts.Logic.Blocks
{
    public class Explosion : MonoBehaviour, ICollisionInteractable
    {
        private IGameGridService _gameGridService;
        private Block _block;
        private ExplosionConfig _explosionConfig;
        private IEffectFactory _effectFactory;

        public void Construct(
            IGameGridService gameGridService, 
            Block block, 
            ExplosionConfig explosionConfig, 
            IEffectFactory effectFactory)
        {
            _gameGridService = gameGridService;
            _block = block;
            _explosionConfig = explosionConfig;
            _effectFactory = effectFactory;
        }

        public async void Interact()
        {
            _gameGridService.RemoveAt(_block.GridPosition);
            
            int length = _explosionConfig.Length == -1
                ? _gameGridService.GridSize.x + _gameGridService.GridSize.y
                : _explosionConfig.Length;
            
            for (int i = 1; i <= length; i++)
            {
                bool withinGrid = false;
                IterateDirections(ref withinGrid, i);

                if (!withinGrid)
                {
                    return;
                }
                
                await Task.Delay((int)(_explosionConfig.SecondsPerWave * 1000));
            }
        }

        private void IterateDirections(ref bool withinGrid, int directionMultiplier)
        {
            foreach (var point in _explosionConfig.Directions)
            {
                Vector2Int endPosition = _block.GridPosition + (point * directionMultiplier);
                if (_gameGridService.TryGetWorldPosition(out Vector2 worldPosition, endPosition))
                {
                    withinGrid = true;
                    SpawnContext spawnContext = new SpawnContext { Position = worldPosition };
                    Effect effect = _effectFactory.Spawn(spawnContext);
                    effect.EnableEffect(_explosionConfig.ExplosionEffectKey);
                }

                if (_gameGridService.TryGetBlock(out Block block, endPosition))
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