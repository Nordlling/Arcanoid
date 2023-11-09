using Main.Scripts.Factory;
using Main.Scripts.Pool;
using UnityEngine;

namespace Main.Scripts.Logic.Blocks
{
    public class Block : SpawnableItemMono
    {
        public SpriteRenderer SpriteRenderer => _spriteRenderer;
        public BoxCollider2D Collider => _collider;
        public Health Health => _health;

        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private BoxCollider2D _collider;
        [SerializeField] private Health _health;
       
        private IBlockFactory _blockFactory;

        public void Construct(IBlockFactory blockFactory)
        {
            _blockFactory = blockFactory;
        }

        private void Start()
        {
            _health.OnDied += Die;
        }

        private void Die()
        {
            _blockFactory.Despawn(this);
        }
    }
}